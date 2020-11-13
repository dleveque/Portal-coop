<div align="center">
  <img src="Assets/logo.png" alt="drawing" width="200px;"/>
  <h1><b>Portal Coop</b></h1>
  <h3>Environnement collaboratif dans l'univers d'Aperture Science</h3>
</div>

</br></br>

# Description du projet

Projet réalisé au sein du module IEVA (Interaction avec les Environnements de réalité Virtuelle ou Augmentée) de mon double diplome SIIA (Système Interactif Intelligent et Autonome).
Il était question dans ce projet de réussir à mettre en place des interactions multi-utilisateurs dans un monde virtuel. Par exemple en mettant en place des interactions avec des objets qui ne peuvent pas être faites à un seul utilisateur.

Pour réaliser cela, on a utilisé Unity3D pour créer le monde virtuel et Photon pour synchroniser l'ensemble des actions à travers les différentes machines.

> ### [Une version en WebGL du projet est disponible ici](https://dleveque.github.io/Portal-coop/) 
> Pour jouer, il suffit d'ouvrir dans deux fenêtres ce même lien.

# Videos de démonstration

<video width=600  height=300 controls>
  <source src="Portal-coop.mp4" type="video/mp4">
</video>

# But du jeu

Vous vous trouvez dans une salle de test du célèbre laboratoire d'Aperture Science. Ce test est destiné à évaluer la capacité des humains à pouvoir interagir et collaborer au sein d'un monde virtuel à travers leurs avatars respectifs.

__Vous devrez analyser les différents éléments qui composent la pièce, afin de comprendre comment sortir de la pièce et rejoindre l'ascenseur situé de l'autre côté de la porte.__

Sachez que le cube et le joueur ont une masse, que le gros bouton près de la porte est sensible au poids et que le champ de forces détruit seulement les objets non autorisés (comme les cubes) mais pas les joueurs.

De plus une salle de synchronisation a été ajoutée derrière la première porte. C'est la suivante qui donne accès à l'ascenseur. Ceci permet d'éviter le sacrifice d'un des joueurs et de vérifier que ce dernier ne reste pas sur le gros bouton pour laisser les joueurs sortir de la zone de test.

Saurez-vous résoudre l'énigme et rejoindre l'ascenseur ?

# Contrôle

> - Flèches directionnelles pour se déplacer
> - Souris + click gauche maintenu pour déplacer le curseur
> - Touche B pour interagir / agripper un objet
> - Touche N pour relâcher l'objet
> - Touche C pour envoyer un message d'aide à porter
> - Touche V pour envoyer un message de viser

# Choix du type de co-manipulation

Pour l'objet à manipuler, j'ai choisi de conserver l'exemple qui a été fourni en début de projet avec un cube. J'ai par la suite remplacé ce dernier par un modèle 3D.

Pour réussir à faire une co-manipulation du cube, j'ai contraint ce dernier avec une notion de poids. Chaque utilisateur est muni d'un outil, un générateur antigravitationnel, leur permettant de pouvoir retirer une certaine quantité de masse au cube pour pouvoir le soulever. Bien entendu, plus il a d'outil qui manipule le cube, plus on peut le soulever haut.

Tous les utilisateurs peuvent effectuer les mêmes actions sur le cube. Leurs actions sont moyennées pour positionner le cube dans l'espace et sommer pour la rotation de l'objet.

# Awareness d'interaction 

L'utilisateur peut interagir sur les objets disposés dans l'environnement à l'aide son curseur.
Cette zone d'interaction est mise en surbrillance lorsque le curseur est dedans. Elle se colore en bleu pour signifier que l'utilisateur la survole et en orange lorsqu'il interagit avec.
De plus un pop-up apparaît en bas de l'écran pour indiquer l'action à effectuer pour interagir avec l'objet.

Par exemple pour le bouton situé dans la pièce, lorsqu'on approche son curseur sur son sommet, une sphère bleue apparaît avec un message indiquant que pour interagir avec, il faut appuyer sur la touche 'B'.
De même pour manipuler le cube via l'une de ses interfaces disposées sur chacune de ses faces ('B' pour attraper et 'N' pour relâcher)

Pour faire comprendre que l'objet est trop lourd, j'ai choisi qu'à partir d'une certaine hauteur le cube se mettrait à vibrer pour simuler le fait que le générateur antigravitationnel n'a plus la capacité de le soulever. Si l'utilisateur persiste, l'interface sur laquelle il interagit s'éloigne du cube et sort de sa portée. Le cube retrouve les effets de pesanteur et retombe au sol.
L'utilisateur est alors contraint à appeler à l'aide un autre utilisateur pour l'aider.

# Envoi de message d'aide

Pour envoyé un message d'aide, il est possible d'envoyer à tout moment des messages sous forme d'icône dans l'environnement virtuel. Dans cette version, deux icônes sont présentes:
  
  - <img src="Assets/Resources/PortalElements/MessageIcon/carry.png" width="40px"/> Permet d'inciter l'autre utilisateur à manipuler lui aussi le cube. Envoi possible en appuyant sur la touche 'C'.
  - <img src="Assets/Resources/PortalElements/MessageIcon/point.png" width="40px"/> Permet d'indiquer à l'autre utilisateur une zone à regarder en particulier. Envoi possible en appuyant sur la touche 'V'.

Une fois envoyée, l'icône se dirigera dans la direction que pointe l'outil et s'arrêtera au premier obstacle solide. De plus l'icône prendra la couleur de l'utilisateur qui l'a envoyé pour indiquer aux autres l'expéditeur du message.

Cette couleur est attribuée automatiquement dès le début de la partie et s'applique sur l'outil et le curseur.

# Salle de test

À force de manipuler un cube lors de la conception du projet, cela m'a fait rappeler un certain jeux vidéo éditer par Valve du nom de Portal. Il s'agit d'un puzzle game où le joueur est amené à résoudre des énigmes pour passer de salle en salle. Souhaitant un jour faire un jeu avec Unity et sachant que le sujet du projet était de réaliser un défi à plusieurs utilisateurs, j'ai décidé d'en faire un peu plus. D'autant plus que la communauté avait créé un nombre incroyable de modèle 3D gratuit et en libre accès.

J'ai donc rajouté au fur et à mesure des modèles 3D et textures pour améliorer l'immersion et l'interaction. En effet en faisant tester le niveau auprès de mon entourage, je me suis rendu compte qu'il était assez courant que l'utilisateur perde son curseur. J'ai donc décidé au lieu de contraindre le curseur à ne pas sortir du champ de vision, à ajouter un modèle 3D de générateur qui fixe le curseur. Ainsi l'utilisateur sait en regardant son outil, où se trouve son curseur.



# 📂 Contenu du répertoire

    |-Assets: L'ensemble des assets du monde 3D
    |-Build: Build du projet sous WebGl
    |-Portal-coop.mp4: vidéo de démonstration
    |-ReadMe.md: micro rapport 

# 📦 Installation du projet

1. Installer Unity3D dans sa version 2019.3.0f3 [Unity](https://unity3d.com/fr/get-unity/download/archive)

2. Cloner le dépôt GitLab sur votre répertoire
```bash
git clone https://github.com/dleveque/Portal-coop.git
```

3. Se rendre dans le répertoire à l'aide d'un terminal
```bash
cd Portal-coop
```

# 📜 Execution du projet

Pour executer le projet, il suffit d'ajouter le projet à Unity Hub et de le lancer dans la bonne version