DROP DATABASE IF EXISTS fleurs; 
CREATE DATABASE IF NOT EXISTS fleurs; 
USE fleurs;

DROP TABLE IF EXISTS clients;
CREATE  TABLE IF NOT EXISTS clients
(id_client VARCHAR(40) PRIMARY KEY,
nom_client VARCHAR(40),
prenom_client VARCHAR(40),
num_tel_client VARCHAR(40),
email_client VARCHAR(40),
mdp_client VARCHAR(40),
adresse_facturation_client VARCHAR(40),
carte_credit_client VARCHAR(40),
nb_commandes INTEGER,
nb_commandes_mois INTEGER,
statut VARCHAR(40)
);


DROP TABLE IF EXISTS bouquet_perso;
CREATE  TABLE IF NOT EXISTS bouquet_perso
(id_perso VARCHAR(40) PRIMARY KEY,
description_bouquet VARCHAR(40),
prix_max FLOAT
);

DROP TABLE IF EXISTS bouquet_std;
CREATE  TABLE IF NOT EXISTS bouquet_std
(id_bouquet VARCHAR(40) PRIMARY KEY,
categorie VARCHAR(40),
prix FLOAT,
nom_bouquet VARCHAR(40)
);


DROP TABLE IF EXISTS commande;
CREATE  TABLE IF NOT EXISTS commande
(num_commande VARCHAR(40) PRIMARY KEY,
date_commande DATETIME,
adresse_livraison VARCHAR(40),
message VARCHAR(40),
date_livraison DATETIME,
etat_commande VARCHAR(40),
id_client VARCHAR(40), FOREIGN KEY(id_client) REFERENCES clients(id_client),
id_perso VARCHAR(40), FOREIGN KEY(id_perso) REFERENCES bouquet_perso(id_perso),
id_bouquet VARCHAR(40), FOREIGN KEY(id_bouquet) REFERENCES bouquet_std(id_bouquet)
);


DROP TABLE IF EXISTS accessoire;
CREATE  TABLE IF NOT EXISTS accessoire
(id_accessoire VARCHAR(40) PRIMARY KEY,
prix_accessoire FLOAT,
dispo_accessoire BOOL
);

DROP TABLE IF EXISTS fleur;
CREATE  TABLE IF NOT EXISTS fleur
(id_fleur VARCHAR(40) PRIMARY KEY,
couleur VARCHAR(40),
prix_fleur FLOAT,
dispo_fleur BOOL
);


DROP TABLE IF EXISTS composition;
CREATE  TABLE IF NOT EXISTS composition
(prix_composition FLOAT,
id_accessoire VARCHAR(40), FOREIGN KEY(id_accessoire) REFERENCES accessoire(id_accessoire),
id_fleur VARCHAR(40), FOREIGN KEY(id_fleur) REFERENCES fleur(id_fleur),
id_perso VARCHAR(40), FOREIGN KEY(id_perso) REFERENCES bouquet_perso(id_perso),
id_bouquet VARCHAR(40), FOREIGN KEY(id_bouquet) REFERENCES bouquet_std(id_bouquet),
PRIMARY KEY(id_accessoire,id_fleur,id_perso,id_bouquet)
);

DROP TABLE IF EXISTS magasin;
CREATE  TABLE IF NOT EXISTS magasin
(id_magasin VARCHAR(40) PRIMARY KEY,
nom_magasin VARCHAR(40),
adresse_magasin VARCHAR(40),
chiffre_affaires FLOAT
);


DROP TABLE IF EXISTS stockfleur;
CREATE  TABLE IF NOT EXISTS stockfleur
(quantite FLOAT,
id_fleur VARCHAR(40), FOREIGN KEY(id_fleur) REFERENCES fleur(id_fleur),
id_magasin VARCHAR(40), FOREIGN KEY(id_magasin) REFERENCES magasin(id_magasin),
PRIMARY KEY(id_fleur,id_magasin)
);

DROP TABLE IF EXISTS stockaccessoire;
CREATE  TABLE IF NOT EXISTS stockaccessoire
(quantite FLOAT,
id_accessoire VARCHAR(40), FOREIGN KEY(id_accessoire) REFERENCES accessoire(id_accessoire),
id_magasin VARCHAR(40), FOREIGN KEY(id_magasin) REFERENCES magasin(id_magasin),
PRIMARY KEY(id_accessoire,id_magasin)
);


DROP TABLE IF EXISTS achat_dans;
CREATE  TABLE IF NOT EXISTS achat_dans
(id_client VARCHAR(40), FOREIGN KEY(id_client) REFERENCES clients(id_client),
id_magasin VARCHAR(40), FOREIGN KEY(id_magasin) REFERENCES magasin(id_magasin),
PRIMARY KEY(id_client,id_magasin)
);
