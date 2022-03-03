# EncyclopeBeer/WikiBeer

Projet fil rouge POEC DEVOPS .Net  

Clément/Armel  

## Introduction  

Dans le cadre de la formation, nous devons réaliser un projet en binôme.  
L'application doit se comporter :  

- Un client lourd permettant la lecture, l'ajout et la modification d'un item.  

- Un client web permettant d'administrer les utilisateurs et leurs droits.  

- Un Back-End sécurisé permettant de traiter les opérations de gestion de votre item ainsi que les opérations relatives à la gestion des utilisateurs.  

Ce back-end s'appuiera sur une base de données relationnelles pour stocker les données nécessaires à son bon fonctionnement.  
La communication avec le back-end sera faite en WCF pour le client lourd et via Web API(REST) pour le client web.  
Toute opération effectuée sur les back-end sera mise à disposition via un broker de message.  
Ce projet passera par une phase de conception de réalisation et de présentation de votre production.  
Ce projet nous permettra d'acquérir les connaissances et compétences nécessaires pour développer une application moderne en .Net.

Le projet doit également remplir certaines conditions :  

- Comporter 3/5 entités  
- Une relation *One-to-One*  
- Une relation *One-to-Many* ou *Many-to-Many*

## Présentation du projet  

L'encyclopebeer est une application permettant aux amateurs de bières de rechercher une bière, noter et garder en mémoire leurs bières favorites.
  
Pour un professionnel, elle permet de connaître le profil des utilisateurs : leur goûts et préférences afin de pouvoir faire des suggestions personnalisées ou générales en fonction de la communauté.

## Fonctionnalités

L'objet principal de l'application sera "Beer".  

Il existe deux type d'utilisateur :  

- **Utilisateur Standard**: utilisateurs principaux de l'application via l'application Web, ils peuvent :  

  - Consulter la liste des bières et effectuer une recherche
  - Se connecter a l'application pour :     
    - Ajouter une bière a ses favoris  
  
- **Administateur**: Capable de gérer l'application via le client lourd.

  - Effectuer des oppérations CRUD sur les bières et leurs sous objets
  - Administrer les utilisateurs (donner les droits d'accès aux client lourds)

## Ce que l'on aimerait rajouter

Il devra exister trois types d'utilisateurs dui devront pouvoir effectuer en plus les actions suivantes : 

- **Utilisateur Standart** :
  
  - Rechercher d'autre utilisateurs pour : 
    - Les ajouter en amis
    - Leur proposer une bière

- **Utilisateur Vérifié**

  - Se connecter à l'application pour : 
    - Rediger un commentaire sur une bière
    - Noter une bière.  
    - Proposer l'ajout d'une bière aux Administrateurs 

- **Administateur**: Capable de gérer l'application via le client lourd.

 ## Lancer le projet
 
 Télécharger le dossier Wikibeer puis : 
 
 ### Première utilisation 
 
- Ouvrir le .sln et le compiler.
- Lancer l'API : à partir du dossier API : 
```
dotnet run 
```
- Lancer ConsoleAppPopulateTest pour créer et peupler la base de donnée : 
```
dotnet run 
```
dans ConsoleAppPopulateTest.

### Utilisation courante

- Lancer l'API : à partir du dossier API : 
```
dotnet run 
```
- Pour démarer le client lourd : 
```
dotnet run 
```
dans le dossier Wpf.
- pour lancer le client Web : à partir du dossier Web : 
```
npm install 
```
si première utilisation, puis 
```
ng serve --open
```

### Note sur le client lourd

Le client lourd (Wpf) nécessite une connection via Auth0. Pour le lancer sans il faut simplement désactiver la partie concerner dans le fichier Wpf/UserControls/Views/PrimaryViews/ViewLogin.xaml.cs ou bien contacter un des gestionnaires du projet.

### Note sur le client Web

Le client Web nécessite une création de compte avant de pouvoir utiliser les fonctionnalité dédiées aux utilisateurs connectés. La création de compte nécessite seulement un clique sur le bouton correspondant dans le client Web. La suppression d'un compte peut être demandé aux gestionnaires du projet.

## Bug connus
- Client lourd (WPf)
  - Un regex ne fonctionne pas comme attendue sur les champs attendant des floats. Ces champs n'acceptent que des int pour l'instant. Contrournement : passer par la ConsoleAppPopulateTest pour rentrer/modifier ces champs.
  - La tentative de connection très rapide peut fair eplanter le programme. Il faut attendre l'ouverture et la fermeture de la fenêtre Auth0 pour éviter cela lors de la procédure d'ideentification.
  - Pour le moment la tentative de connection d'un utilisateur non certifié ne provoquera qu'une redirection vers la page de login donnant l'illusion que rien ne se passe. It's not a Bug, it's a feature.

- Client léger (Web)
  - En cas de connexion très lente, il se peut que le programme bug et ne présente plus qu'une seule bière. Actualiser la page règle le problème.
  - Lors de la connection, il faut recliquer sur le bouton Bières pour afficher la liste (problème de navigation dans la configuration de Auth0).
  - La bar de recherche n'est pas aussi intelligente que l'on voudrait, cette dernière fait une recherche par inclusion de charactère et pas de string.
