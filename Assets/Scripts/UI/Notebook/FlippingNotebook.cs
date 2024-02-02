//The implementation is based on this article:http://rbarraza.com/html5-canvas-pageflip/
//As the rbarraza.com website is not live anymore you can get an archived version from web archive 
//or check an archived version that I uploaded on my website: https://dandarawy.com/html5-canvas-pageflip/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;

public enum FlipMode
{
    RightToLeft,
    LeftToRight
}
[ExecuteInEditMode]
public class FlippingNotebook : MonoBehaviour {
    [Header("General References")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform BookPanel;
    [SerializeField] private Sprite background;
    [SerializeField] private Image _clippingPlane;
    [SerializeField] private Image _nextPageClip;
    [SerializeField] private Image _shadow;
    [SerializeField] private Image _shadowLTR;
    [SerializeField] private Image _left;
    [SerializeField] private Image _leftNext;
    [SerializeField] private Image _right;
    [SerializeField] private Image _rightNext;
    [SerializeField] private UIEventsPublisher _uiEvents;
    [SerializeField] private NotebookManager _notebookManager;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    
    [Header("Pages References")]
    [SerializeField] private List<Sprite> _currentBookPages;
    [SerializeField] private List<Sprite> _bookPagesPlanned;
    
    [Header("Parameters")]
    [SerializeField] private bool enableShadowEffect=true;
    [SerializeField] private float _basePageFlipTime = 0.2f;
    [SerializeField] private int _baseAnimationFramesCount = 20;
    [SerializeField] private float _fastPageFlipTime = 0.2f;
    [SerializeField] private int _fastAnimationFramesCount = 20;

    private float _pageFlipTime, _animationFramesCount;
    private int _currentAddedPagesIndex;
    
    private float _radius1, _radius2;
    //Spine Bottom
    private Vector3 _sb;
    //Spine Top
    private Vector3 _st;
    //corner of the page
    private Vector3 _c;
    //Edge Bottom Right
    private Vector3 _ebr;
    //Edge Bottom Left
    private Vector3 _ebl;
    //follow point 
    private Vector3 _f;
    private bool _pageDragging = false;
    //current flip mode
    private FlipMode _mode;
    private Coroutine _currentCoroutine;
    private int _currentPage = 0;
    private bool _isFlipping;
    
    public int TotalPageCount { get { return _currentBookPages.Count; } }
    public bool IsFlipping { get { return _isFlipping; } }
    public int CurrentPage { get { return _currentPage; } }
    public Vector3 EndBottomLeft { get { return _ebl; } }
    public Vector3 EndBottomRight { get { return _ebr; } }

    private void Awake()
    {
        _pageFlipTime = _basePageFlipTime;
        _animationFramesCount = _baseAnimationFramesCount;
        _playerEvents.AddingPages.AddListener(AddPages);
    }

    void Start()
    {
        if (!canvas) canvas=GetComponentInParent<Canvas>();
        if (!canvas) Debug.LogError("Book should be a child to canvas");

        _left.gameObject.SetActive(false);
        _right.gameObject.SetActive(false);
        UpdateSprites();
        CalcCurlCriticalPoints();

        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        _nextPageClip.rectTransform.sizeDelta = new Vector2(pageWidth, pageHeight + pageHeight * 2);
        _clippingPlane.rectTransform.sizeDelta = new Vector2(pageWidth * 2 + pageHeight, pageHeight + pageHeight * 2);

        //hypotenous (diagonal) page length
        float hyp = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
        float shadowPageHeight = pageWidth / 2 + hyp;

        _shadow.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        _shadow.rectTransform.pivot = new Vector2(1, (pageWidth / 2) / shadowPageHeight);

        _shadowLTR.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        _shadowLTR.rectTransform.pivot = new Vector2(0, (pageWidth / 2) / shadowPageHeight);
        
    }
    
    private void PageFlipped()
    {
        if (_currentPage == 0)_uiEvents.NotebookFlipped.Invoke(true, false);
        else if (_currentPage == TotalPageCount)_uiEvents.NotebookFlipped.Invoke(false, true);
        else _uiEvents.NotebookFlipped.Invoke(false, false);
        _isFlipping = false;
    }

    public void FlipToEnd()
    {
        if(!_isFlipping) StartCoroutine(FlipToPage(TotalPageCount));
    }

    public void FlipToBeginning()
    {
        if(!_isFlipping) StartCoroutine(FlipToPage(0));
    }
    
    public void FlipToNotes()
    {
        if(!_isFlipping) StartCoroutine(FlipToPage(2));
    }
    private IEnumerator FlipToPage(int pageNumber)
    {
        _pageFlipTime = _fastPageFlipTime;
        _animationFramesCount = _fastAnimationFramesCount;
        while (_currentPage != pageNumber)
        {
            if (_currentPage < pageNumber)
            {
                FlipRightPage();
            }
            else
            {
                FlipLeftPage();
            }
            yield return new WaitUntil(() => !_isFlipping);
        }
        _pageFlipTime = _basePageFlipTime;
        _animationFramesCount = _baseAnimationFramesCount;
    }

    public void AddPages(int pagesToAdd)
    {
        Debug.Log("adding page");
            for (int i = 0; i < pagesToAdd; i++)
            {
                if (_currentAddedPagesIndex < _currentBookPages.Count - 1)
                {
                    _currentAddedPagesIndex++;
                    _currentBookPages[_currentAddedPagesIndex] = _bookPagesPlanned[_currentAddedPagesIndex];
                }
            }
            UpdateSprites();
        
    }
    
    public void FlipRightPage()
    {
        if (_isFlipping) return;
        if (_currentPage >= TotalPageCount) return;
        _isFlipping = true;
        float frameTime = _pageFlipTime / _animationFramesCount;
        float xc = (EndBottomRight.x + EndBottomLeft.x) / 2;
        float xl = ((EndBottomRight.x - EndBottomLeft.x) / 2) * 0.9f;
        float h = Mathf.Abs(EndBottomRight.y) * 0.9f;
        float dx = (xl)*2 / _animationFramesCount;
        StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
    }
    
    public void FlipLeftPage()
    {
        if (_isFlipping) return;
        if (_currentPage <= 0) return;
        if (_currentPage == TotalPageCount) _notebookManager.HidePostcards();
        _isFlipping = true;
        float frameTime = _pageFlipTime / _animationFramesCount;
        float xc = (EndBottomRight.x + EndBottomLeft.x) / 2;
        float xl = ((EndBottomRight.x - EndBottomLeft.x) / 2) * 0.9f;
        float h = Mathf.Abs(EndBottomRight.y) * 0.9f;
        float dx = (xl) * 2 / _animationFramesCount;
        StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
    }
    
    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

        DragRightPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < _animationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            UpdateBookRTLToPoint(new Vector3(x, y, 0));
            yield return new WaitForSecondsRealtime(frameTime);
            x -= dx;
        }
        ReleasePage();
    }
    IEnumerator FlipLTR(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc - xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);
        DragLeftPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < _animationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            UpdateBookLTRToPoint(new Vector3(x, y, 0));
            yield return new WaitForSecondsRealtime(frameTime);
            x += dx;
        }
        ReleasePage();
    }
    
    private void CalcCurlCriticalPoints()
    {
        _sb = new Vector3(0, -BookPanel.rect.height / 2);
        _ebr = new Vector3(BookPanel.rect.width / 2, -BookPanel.rect.height / 2);
        _ebl = new Vector3(-BookPanel.rect.width / 2, -BookPanel.rect.height / 2);
        _st = new Vector3(0, BookPanel.rect.height / 2);
        _radius1 = Vector2.Distance(_sb, _ebr);
        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        _radius2 = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
    }
    
    public void UpdateBookLTRToPoint(Vector3 followLocation)
    {
        _mode = FlipMode.LeftToRight;
        _f = followLocation;
        _shadowLTR.transform.SetParent(_clippingPlane.transform, true);
        _shadowLTR.transform.localPosition = new Vector3(0, 0, 0);
        _shadowLTR.transform.localEulerAngles = new Vector3(0, 0, 0);
        _left.transform.SetParent(_clippingPlane.transform, true);

        _right.transform.SetParent(BookPanel.transform, true);
        _right.transform.localEulerAngles = Vector3.zero;
        _leftNext.transform.SetParent(BookPanel.transform, true);

        _c = Calc_C_Position(followLocation);
        Vector3 t1;
        float clipAngle = CalcClipAngle(_c, _ebl, out t1);
        //0 < T0_T1_Angle < 180
        clipAngle = (clipAngle + 180) % 180;

        _clippingPlane.transform.localEulerAngles = new Vector3(0, 0, clipAngle - 90);
        _clippingPlane.transform.position = BookPanel.TransformPoint(t1);

        //page position and angle
        _left.transform.position = BookPanel.TransformPoint(_c);
        float C_T1_dy = t1.y - _c.y;
        float C_T1_dx = t1.x - _c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;
        _left.transform.localEulerAngles = new Vector3(0, 0, C_T1_Angle - 90 - clipAngle);

        _nextPageClip.transform.localEulerAngles = new Vector3(0, 0, clipAngle - 90);
        _nextPageClip.transform.position = BookPanel.TransformPoint(t1);
        _leftNext.transform.SetParent(_nextPageClip.transform, true);
        _right.transform.SetParent(_clippingPlane.transform, true);
        _right.transform.SetAsFirstSibling();

        _shadowLTR.rectTransform.SetParent(_left.rectTransform, true);
    }
    public void UpdateBookRTLToPoint(Vector3 followLocation)
    {
        _mode = FlipMode.RightToLeft;
        _f = followLocation;
        _shadow.transform.SetParent(_clippingPlane.transform, true);
        _shadow.transform.localPosition = Vector3.zero;
        _shadow.transform.localEulerAngles = Vector3.zero;
        _right.transform.SetParent(_clippingPlane.transform, true);

        _left.transform.SetParent(BookPanel.transform, true);
        _left.transform.localEulerAngles = Vector3.zero;
        _rightNext.transform.SetParent(BookPanel.transform, true);
        _c = Calc_C_Position(followLocation);
        Vector3 t1;
        float clipAngle = CalcClipAngle(_c, _ebr, out t1);
        if (clipAngle > -90) clipAngle += 180;

        _clippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);
        _clippingPlane.transform.localEulerAngles = new Vector3(0, 0, clipAngle + 90);
        _clippingPlane.transform.position = BookPanel.TransformPoint(t1);

        //page position and angle
        _right.transform.position = BookPanel.TransformPoint(_c);
        float C_T1_dy = t1.y - _c.y;
        float C_T1_dx = t1.x - _c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;
        _right.transform.localEulerAngles = new Vector3(0, 0, C_T1_Angle - (clipAngle + 90));

        _nextPageClip.transform.localEulerAngles = new Vector3(0, 0, clipAngle + 90);
        _nextPageClip.transform.position = BookPanel.TransformPoint(t1);
        _rightNext.transform.SetParent(_nextPageClip.transform, true);
        _left.transform.SetParent(_clippingPlane.transform, true);
        _left.transform.SetAsFirstSibling();

        _shadow.rectTransform.SetParent(_right.rectTransform, true);
    }
    private float CalcClipAngle(Vector3 c,Vector3 bookCorner,out  Vector3 t1)
    {
        Vector3 t0 = (c + bookCorner) / 2;
        float T0_CORNER_dy = bookCorner.y - t0.y;
        float T0_CORNER_dx = bookCorner.x - t0.x;
        float T0_CORNER_Angle = Mathf.Atan2(T0_CORNER_dy, T0_CORNER_dx);
        float T0_T1_Angle = 90 - T0_CORNER_Angle;
        
        float T1_X = t0.x - T0_CORNER_dy * Mathf.Tan(T0_CORNER_Angle);
        T1_X = normalizeT1X(T1_X, bookCorner, _sb);
        t1 = new Vector3(T1_X, _sb.y, 0);
        
        //clipping plane angle=T0_T1_Angle
        float T0_T1_dy = t1.y - t0.y;
        float T0_T1_dx = t1.x - t0.x;
        T0_T1_Angle = Mathf.Atan2(T0_T1_dy, T0_T1_dx) * Mathf.Rad2Deg;
        return T0_T1_Angle;
    }
    private float normalizeT1X(float t1,Vector3 corner,Vector3 sb)
    {
        if (t1 > sb.x && sb.x > corner.x)
            return sb.x;
        if (t1 < sb.x && sb.x < corner.x)
            return sb.x;
        return t1;
    }
    private Vector3 Calc_C_Position(Vector3 followLocation)
    {
        Vector3 c;
        _f = followLocation;
        float F_SB_dy = _f.y - _sb.y;
        float F_SB_dx = _f.x - _sb.x;
        float F_SB_Angle = Mathf.Atan2(F_SB_dy, F_SB_dx);
        Vector3 r1 = new Vector3(_radius1 * Mathf.Cos(F_SB_Angle),_radius1 * Mathf.Sin(F_SB_Angle), 0) + _sb;

        float F_SB_distance = Vector2.Distance(_f, _sb);
        if (F_SB_distance < _radius1)
            c = _f;
        else
            c = r1;
        float F_ST_dy = c.y - _st.y;
        float F_ST_dx = c.x - _st.x;
        float F_ST_Angle = Mathf.Atan2(F_ST_dy, F_ST_dx);
        Vector3 r2 = new Vector3(_radius2 * Mathf.Cos(F_ST_Angle),
           _radius2 * Mathf.Sin(F_ST_Angle), 0) + _st;
        float C_ST_distance = Vector2.Distance(c, _st);
        if (C_ST_distance > _radius2)
            c = r2;
        return c;
    }
    public void DragRightPageToPoint(Vector3 point)
    {
        if (_currentPage >= _currentBookPages.Count) return;
        _pageDragging = true;
        _mode = FlipMode.RightToLeft;
        _f = point;
        
        _nextPageClip.rectTransform.pivot = new Vector2(0, 0.12f);
        _clippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);

        _left.gameObject.SetActive(true);
        _left.rectTransform.pivot = new Vector2(0, 0);
        _left.transform.position = _rightNext.transform.position;
        _left.transform.eulerAngles = new Vector3(0, 0, 0);
        _left.sprite = (_currentPage < _currentBookPages.Count) ? _currentBookPages[_currentPage] : background;
        _left.transform.SetAsFirstSibling();
        
        _right.gameObject.SetActive(true);
        _right.transform.position = _rightNext.transform.position;
        _right.transform.eulerAngles = new Vector3(0, 0, 0);
        _right.sprite = (_currentPage < _currentBookPages.Count - 1) ? _currentBookPages[_currentPage + 1] : background;

        _rightNext.sprite = (_currentPage < _currentBookPages.Count - 2) ? _currentBookPages[_currentPage + 2] : background;

        _leftNext.transform.SetAsFirstSibling();
        if (enableShadowEffect) _shadow.gameObject.SetActive(true);
        UpdateBookRTLToPoint(_f);
    }

    public void DragLeftPageToPoint(Vector3 point)
    {
        if (_currentPage <= 0) return;
        _pageDragging = true;
        _mode = FlipMode.LeftToRight;
        _f = point;

        _nextPageClip.rectTransform.pivot = new Vector2(1, 0.12f);
        _clippingPlane.rectTransform.pivot = new Vector2(0, 0.35f);

        _right.gameObject.SetActive(true);
        _right.transform.position = _leftNext.transform.position;
        _right.sprite = _currentBookPages[_currentPage - 1];
        _right.transform.eulerAngles = new Vector3(0, 0, 0);
        _right.transform.SetAsFirstSibling();

        _left.gameObject.SetActive(true);
        _left.rectTransform.pivot = new Vector2(1, 0);
        _left.transform.position = _leftNext.transform.position;
        _left.transform.eulerAngles = new Vector3(0, 0, 0);
        _left.sprite = (_currentPage >= 2) ? _currentBookPages[_currentPage - 2] : background;

        _leftNext.sprite = (_currentPage >= 3) ? _currentBookPages[_currentPage - 3] : background;

        _rightNext.transform.SetAsFirstSibling();
        if (enableShadowEffect) _shadowLTR.gameObject.SetActive(true);
        UpdateBookLTRToPoint(_f);
    }
    
    public void ReleasePage()
    {
        if (_pageDragging)
        {
            _pageDragging = false;
            float distanceToLeft = Vector2.Distance(_c, _ebl);
            float distanceToRight = Vector2.Distance(_c, _ebr);
            if (distanceToRight < distanceToLeft && _mode == FlipMode.RightToLeft)
                TweenBack();
            else if (distanceToRight > distanceToLeft && _mode == FlipMode.LeftToRight)
                TweenBack();
            else
                TweenForward();
        }
    }
    
    void UpdateSprites()
    {
        _leftNext.sprite= (_currentPage > 0 && _currentPage <= _currentBookPages.Count) ? _currentBookPages[_currentPage-1] : background;
        _rightNext.sprite=(_currentPage>=0 &&_currentPage<_currentBookPages.Count) ? _currentBookPages[_currentPage] : background;
    }
    public void TweenForward()
    {
        if(_mode== FlipMode.RightToLeft)
            _currentCoroutine = StartCoroutine(TweenTo(_ebl, 0.15f, () => { Flip(); }));
        else
            _currentCoroutine = StartCoroutine(TweenTo(_ebr, 0.15f, () => { Flip(); }));
    }
    
    void Flip()
    {
        if (_mode == FlipMode.RightToLeft)
            _currentPage += 2;
        else
            _currentPage -= 2;
        

        
        _leftNext.transform.SetParent(BookPanel.transform, true);
        _left.transform.SetParent(BookPanel.transform, true);
        _leftNext.transform.SetParent(BookPanel.transform, true);
        _left.gameObject.SetActive(false);
        _right.gameObject.SetActive(false);
        _right.transform.SetParent(BookPanel.transform, true);
        _rightNext.transform.SetParent(BookPanel.transform, true);
        UpdateSprites();
        _shadow.gameObject.SetActive(false);
        _shadowLTR.gameObject.SetActive(false);
        PageFlipped();
    }
    
    public void TweenBack()
    {
        if (_mode == FlipMode.RightToLeft)
        {
            _currentCoroutine = StartCoroutine(TweenTo(_ebr,0.15f,
                () =>
                {
                    UpdateSprites();
                    _rightNext.transform.SetParent(BookPanel.transform);
                    _right.transform.SetParent(BookPanel.transform);

                    _left.gameObject.SetActive(false);
                    _right.gameObject.SetActive(false);
                    _pageDragging = false;
                }
                ));
        }
        else
        {
            _currentCoroutine = StartCoroutine(TweenTo(_ebl, 0.15f,
                () =>
                {
                    UpdateSprites();

                    _leftNext.transform.SetParent(BookPanel.transform);
                    _left.transform.SetParent(BookPanel.transform);

                    _left.gameObject.SetActive(false);
                    _right.gameObject.SetActive(false);
                    _pageDragging = false;
                }
                ));
        }
    }
    public IEnumerator TweenTo(Vector3 to, float duration, System.Action onFinish)
    {
        int steps = (int)(duration / 0.025f);
        Vector3 displacement = (to - _f) / steps;
        for (int i = 0; i < steps-1; i++)
        {
            if(_mode== FlipMode.RightToLeft)
                UpdateBookRTLToPoint( _f + displacement);
            else
                UpdateBookLTRToPoint(_f + displacement);

            yield return new WaitForSecondsRealtime(0.025f);
        }
        if (onFinish != null)
            onFinish();
    }
}
