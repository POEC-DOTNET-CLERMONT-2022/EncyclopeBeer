Dans CodeFirstDB les tests en cours pour remplacer wcf par l'api rest. 
Pour l'instant la migration et une partie des controllers de l'api fonctionnent mais on a encore des problèmes de sérialisation avec l'api sur les collection de classes abstraites.
En bonne voie cependant.

# EncyclopeBeer

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

## Fonctionnalité  

L'objet principal de l'application sera "Beer".  

Il existera trois type d'utilisateur :  

- **Utilisateur Standard**: utilisateurs principaux de l'application, ils pourront :  

  - Se connecter a l'application
  - Consutlter la liste des bières et effectuer une recherche
  - Noter une bière  
  - Ajouter une bière a ses favoris  

- **Utilisateur Vérifié**: utilisateurs qui disposerons de droits supplémentaires :  
  
  - Proposer l'ajout d'une bière  
  - Rediger un commentaire sur une bière

- **Administateur**: Capable de gérer l'application via le client lourd.

  - Effectuer des oppérations CRUD sur les bières et leurs sous objets
  - Administrer les utilisateurs (gestion des rôles, supprésion de compte, ...)
  - Consutler différents statistiques sur les utilisateurs, les bières ...  


