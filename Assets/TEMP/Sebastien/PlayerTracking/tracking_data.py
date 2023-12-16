import os
import numpy as np
import matplotlib.pyplot as plt

def load_tracking_data(file_path):
    with open(file_path, 'r') as file:
        lines = file.readlines()

    tracking_data = []
    for line in lines:
        values = line.strip().split(',')
        if len(values) >= 3:
            x, y, z = map(float, values)
            tracking_data.append((x, y, z))

    return np.array(tracking_data)

def generate_density_heatmap(all_positions, file_count):
    all_positions = np.vstack(all_positions)
    x, z = all_positions[:, 0], all_positions[:, 2]

    # Définir la taille par défaut de la heatmap
    heatmap_size = 150

    # Calcul de la densité moyenne pour chaque cellule (x, z)
    heatmap, xedges, zedges = np.histogram2d(x, z, bins=(heatmap_size, heatmap_size))
    heatmap /= file_count  # Normalisation pour obtenir la moyenne

    extent = [xedges[0], xedges[-1], zedges[0], zedges[-1]]

    # Utilisation d'une colormap à contraste élevé
    plt.figure(figsize=(1280/80, 720/80))  # Taille par défaut : 1280 par 720
    plt.imshow(heatmap.T, extent=[0, heatmap_size, 0, heatmap_size], cmap=plt.cm.plasma, origin='lower', aspect='auto', interpolation='gaussian', vmax=np.max(heatmap)*0.1)
    
    # Définir le format par défaut des axes x et y
    plt.xticks(np.linspace(0, heatmap_size, 10), rotation=45)
    plt.yticks(np.linspace(0, heatmap_size, 10))
    
    plt.colorbar(label='Point Density')
    plt.title(f'Heatmap of player movement density (X and Z axes) - {file_count} files counted')
    plt.xlabel('X position')
    plt.ylabel('Z position')
    plt.show()




def main():
    tracking_folder_path = 'C:/Users/Megaport/Desktop/PS5/Chroma/Assets/TEMP/Sebastien/PlayerTracking'
    file_count = 0
    all_positions = []

    for file_name in os.listdir(tracking_folder_path):
        if file_name.startswith('PlayerTrackingData') and file_name.endswith('.txt'):
            file_count += 1
            file_path = os.path.join(tracking_folder_path, file_name)
            player_positions = load_tracking_data(file_path)
            all_positions.append(player_positions)

    if file_count > 0:
        generate_density_heatmap(all_positions, file_count)
    else:
        print('Aucun fichier correspondant trouvé.')

if __name__ == "__main__":
    main()
