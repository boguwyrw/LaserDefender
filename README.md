# LaserDefender

The goal of the task was implementing an enemy waves spawning mechanism.
Two classes were created:
1. WavesManager
2. WavesConfiguration

WavesManager class is responsible for creating enemies, waves and for wave spawner mechanism.
WavesConfiguration class is responsible for wave and enemy configurations in Unity Inspector.

Other classes have also been modified. For example: GameScore, SummaryScene.
Enemies prefabs receive tag: "Enemies", to improve searching enemies on the scene.

# Gameplay description

In every wave player has to defeated enemy. Enemies form new wave appears when enemies from previous wave are defeated.
There are two endings of the game. Player can win or lose.
If player lose, in the end of the game will receive information "GAME OVER" and points and also number of completed wave.
If player won, in the end of the game will receive information "YOU WON" and also information about points and number of completed wave.