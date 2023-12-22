using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthNormalsFeature : ScriptableRendererFeature
{
    class RenderPass : ScriptableRenderPass
    {

        private Material material;
        private RTHandle destinationHandle;
        private List<ShaderTagId> shaderTags;
        private FilteringSettings filteringSettings;

        public RenderPass(Material material) : base()
        {
            this.material = material;
            // This contains a list of shader tags. The renderer will only render objects with
            // materials containing a shader with at least one tag in this list
            this.shaderTags = new List<ShaderTagId>() {
                new ShaderTagId("DepthOnly"),
                //new ShaderTagId("SRPDefaultUnlit"),
                //new ShaderTagId("UniversalForward"),
                //new ShaderTagId("LightweightForward"),
            };
            // Render opaque materials
            this.filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
            destinationHandle = RTHandles.Alloc("_DepthNormalsTexture", name: "_DepthNormalsTexture");
        }

        // Configure the pass by creating a temporary render texture and
        // readying it for rendering
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(Shader.PropertyToID(destinationHandle.name), cameraTextureDescriptor, FilterMode.Point);
            ConfigureTarget(destinationHandle);
            ConfigureClear(ClearFlag.All, Color.black);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {

            // Create the draw settings, which configures a new draw call to the GPU
            var drawSettings = CreateDrawingSettings(shaderTags, ref renderingData, renderingData.cameraData.defaultOpaqueSortFlags);
            // We cant to render all objects using our material
            drawSettings.overrideMaterial = material;
            context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filteringSettings);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(Shader.PropertyToID(destinationHandle.name));
        }
    }

    private RenderPass renderPass;

    public override void Create()
    {
        // We will use the built-in renderer's depth normals texture shader
        Material material = CoreUtils.CreateEngineMaterial("Hidden/Internal-DepthNormalsTexture");
        this.renderPass = new RenderPass(material);
        // Render after shadow caster, depth, etc. passes
        renderPass.renderPassEvent = RenderPassEvent.AfterRenderingPrePasses;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(renderPass);
    }
}