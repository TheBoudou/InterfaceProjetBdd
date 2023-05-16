INSERT INTO `fleurs`.`clients` (`id_client`,`nom_client`,`prenom_client`,`num_tel_client`,`email_client`,`mdp_client`,`adresse_facturation_client`,`carte_credit_client`,`nb_commandes`,`nb_commandes_mois`,`Statut`)
VALUES
('C1', 'Durand', 'Jean', '0601020304', 'jean.durand@mail.com', '123456', '1 rue des Fleurs, Paris', '1234 5678 9012 3456', 5, 2, 'vide'),
('C2', 'Dupont', 'Marie', '0603040506', 'marie.dupont@mail.com', 'abcdef', '2 rue des Roses, Lyon', '2345 6789 0123 4567', 8, 4, 'Bronze'),
('C3', 'Martin', 'Luc', '0605060708', 'luc.martin@mail.com', 'ghijkl', '3 rue des Lilas, Marseille', '3456 7890 1234 5678', 12, 3, 'vide'),
('C4', 'Bernard', 'Sophie', '0607080910', 'sophie.bernard@mail.com', 'mnopqr', '4 rue des Pivoines, Bordeaux', '4567 8901 2345 6789', 3, 1, 'Bronze'),
('C5', 'Petit', 'Marc', '0610111213', 'marc.petit@mail.com', 'stuvwx', '5 rue des Coquelicots, Lille', '5678 9012 3456 7890', 7, 2, 'vide'),
('C6', 'Lefebvre', 'Laure', '0612131415', 'laure.lefebvre@mail.com', 'yzabcd', '6 rue des Orchidées, Toulouse', '6789 0123 4567 8901', 9, 4, 'Or'),
('C7', 'Rousseau', 'Julien', '0615161718', 'julien.rousseau@mail.com', 'efghij', '7 rue des Iris, Nantes', '7890 1234 5678 9012', 15, 6, 'vide'),
('C8', 'Marchand', 'Lucie', '0618192021', 'lucie.marchand@mail.com', 'klmnop', '8 rue des Jacinthes, Rennes', '8901 2345 6789 0123', 6, 2, 'vide'),
('C9', 'Blanc', 'Pierre', '0621222324', 'pierre.blanc@mail.com', 'qrstuv', '9 rue des Chrysanthèmes, Nice', '9012 3456 7890 1234', 11, 5, 'vide'),
('C10', 'Girard', 'Sophie', '0624252627', 'sophie.girard@mail.com', 'wxyzab', '10 rue des Dahlias, Montpellier', '0123 4567 8901 2345', 4, 1, 'Or'),
('C11','Meyer','Rubert','0645751218','rubert.meyer@gmail.com','123456','2 rue de seres,Paris','4561 2531 7894 5684',3,0,'Bronze');

INSERT INTO `fleurs`.`clients` (`id_client`,`nom_client`,`prenom_client`,`num_tel_client`,`email_client`,`mdp_client`,`adresse_facturation_client`,`carte_credit_client`,`nb_commandes`,`nb_commandes_mois`,`Statut`)
VALUES
('C12', 'Durandnn', 'Jeannn', '06010203044', 'jean.durandoo@mail.com', '123456', '1 rue des Fleurs, Paris', '1234 5678 9012 3456', 0, 0, 'vide');

INSERT INTO `fleurs`.`bouquet_perso` (`id_perso`,`description_bouquet`,`prix_max`)
VALUES
('Bouquet01', 'Bouquet de roses rouges et blanches', 50.0),
('Bouquet02', 'Bouquet de pivoines et d''oeillets', 45.0),
('Bouquet03', 'Bouquet de tournesols et de gerberas', 40.0),
('Bouquet04', 'Bouquet de lys et de freesias', 55.0),
('Bouquet05', 'Bouquet de roses roses et d''oeillets', 35.0),
('Bouquet06', 'Bouquet de dahlias et de chrysanthèmes', 30.0),
('Bouquet07', 'Bouquet de renoncules et de marguerites', 38.0),
('Bouquet08', 'Bouquet de violettes et de campanules', 42.0),
('Bouquet09', 'Bouquet de pivoines et de roses blanches', 48.0),
('Bouquet10', 'Bouquet de lisianthus et de delphiniums', 52.0),
('Bouquet11', 'Bouquet de roses oranges et de gerberas', 36.0),
('Bouquet12', 'Bouquet de tulipes et de jonquilles', 33.0),
('Bouquet13', 'Bouquet de pivoines et de freesias', 47.0),
('Bouquet14', 'Bouquet de roses rouges et de lys', 58.0),
('Bouquet15', 'Bouquet de renoncules et de freesias', 39.0),
('Bouquet16', 'Bouquet de chrysanthèmes et de dahlias', 31.0),
('Bouquet17', 'Bouquet de roses roses et de lys', 44.0),
('Bouquet18', 'Bouquet de marguerites et de violettes', 41.0),
('Bouquet19', 'Bouquet de tournesols et de lys', 54.0),
('Bouquet20', 'Bouquet de pivoines et de roses rouges', 49.0),
('Bouquet21','Cercle de fleur blanche, des tulipes',25),
('Bouquet22','Croix de fleur jaune et blanche',30),
('Bouquet23','mur de roses ou équivalent',45),
('vide','vide',0);



INSERT INTO `fleurs`.`bouquet_std` (`id_bouquet`,`categorie`,`prix`,`nom_bouquet`)
VALUES
("std1", "St-Valentin", 25.00, "Bouquet de roses rouges"),
("std2", "Enterrement", 30.00, "Bouquet de tulipes multicolores"),
("std3", "Mariage", 40.00, "Bouquet de lys blancs"),
("std4", "Fête des mères", 35.00, "Bouquet de pivoines roses"),
("std5", "Fête des pères", 50.00, "Bouquet d'orchidées blanches"),
("std6", "Toute occasion", 20.00, "Bouquet de marguerites jaunes"),
("std7", "Mariage", 35.00, "Bouquet de lilas mauves"),
("std8", "Anniversaire", 25.00, "Bouquet de gerberas orange"),
("std9", "Anniversaire", 30.00, "Bouquet de chrysanthèmes roses"),
("std10", "Enterrement", 40.00, "Bouquet de dahlias rouges"),
("std11", "Fête des mères", 35.00, "Bouquet d'hortensias bleus"),
('std12','Toute occasion',45,'Gros Merci'),
('vide','vide',0,'vide');


INSERT INTO `fleurs`.`commande` (`num_commande`,`date_commande`,`adresse_livraison`,`message`,`date_livraison`,`etat_commande`,`id_client`,`id_perso`,`id_bouquet`,`prix_commande`)
VALUES
('1', '2023-04-28', '12 Rue de la Paix, Paris', 'Joyeux Anniversaire', '2023-05-30', 'VINV', 'C1', 'vide', 'std1',20),
('2', '2023-04-29', '45 Rue de la Roquette, Lyon', 'Félicitations', '2023-05-20', 'CC', 'C2', 'vide', 'std10',24),
('3', '2023-04-29', '5 Avenue des Champs-Élysées, Paris', 'Bonne Fête', '2023-05-15', 'CPAV', 'C3', 'Bouquet09', 'vide',35),
('4', '2023-04-29', '28 Rue Sainte-Catherine, Bordeaux', 'vide', '2023-05-17', 'CAL', 'C4', 'Bouquet10', 'vide',45),
('5', '2023-04-30', '9 Rue de la République, Marseille', 'Joyeuses Pâques', '2023-05-04', 'CL', 'C5', 'vide', 'std1',32),
('6', '2023-04-30', '1 Place de la Comédie, Montpellier', 'vide', '2023-05-24', 'CAL', 'C6', 'Bouquet06', 'vide',62),
('7', '2023-05-01', '16 Rue des Archives, Paris', 'Bonne Fête des Mères', '2023-05-26', 'CPAV', 'C7', 'Bouquet04','vide',42),
('8', '2023-05-01', '78 Rue de la Pompe, Neuilly-sur-Seine', 'Joyeux Anniversaire', '2023-05-27', 'CC', 'C8', 'vide', 'std4',34),
('9', '2023-05-01', '2 Rue de la République, Lyon', 'vide', '2023-05-30', 'VINV', 'C9', 'vide', 'std3',14),
('10', '2023-05-02', '50 Rue de la République, Lille', 'Bonne Fête des Pères', '2023-05-03', 'CL', 'C9', 'vide', 'std1',35),
('11', '2023-05-02', '10 Rue de la Convention, Marseille', 'vide', '2023-05-24', 'CAL', 'C11', 'vide', 'std5',24),
('12','2002-01-15','15 avenue de serre, Paris','Vite svp','2002-01-20','CL','C10','Bouquet01','vide',26),
('13','2020-01-15','15 avenue de serre, Paris','Allez','2020-01-20','CL','C10','Bouquet02','vide',25),
('14','2021-01-20','15 avenue de serre, Paris','Yes','2021-01-25','CL','C10','Bouquet03','vide',14);





INSERT INTO `fleurs`.`accessoire` (`id_accessoire`,`prix_accessoire`,`dispo_accessoire`)
VALUES 
    ('Petit vase', 12.50, True),
    ('Corde jute', 8.99, True),
    ('Grand vase', 50.00, False),
    ('Bocal verre', 20.00, True),
    ('Cuivre laqué', 15.99, True),
    ('Bulle fleurie', 11.25, False),
    ('Carton carré', 9.99, True),
    ('Attrape rêve', 7.50, True),
    ('Composition chien', 18.75, False),
    ('Récipient zinc', 14.99, True),
    ('Pot',10,True),
    ('vide',0,False);

    
    
INSERT INTO `fleurs`.`fleur` (`id_fleur`,`prix_fleur`,`dispo_fleur`)
VALUES ('rose', 1.99, true),
       ('tulipe', 2.99, true),
       ('lys', 2.50, true),
       ('pivoine', 1.75, false),
       ('orchidée', 3.25, true),
       ('marguerite', 1.99, true),
       ('lila', 0.99, false),
       ('chrysanthème', 1.50, true),
       ('dahlia', 4.50, false),
       ('hortensia', 3.75, true),
       ('violette', 2.25, true),
       ('tournesol', 2.99, true),
       ('oeillet', 3.50, true),
       ('jonquille', 4.50, true),
       ('lavande',3,True),
       ('muguet',2,False),
       ('vide',0,False);
   

       
       
INSERT INTO `fleurs`.`composition` (`quantite`, `id_accessoire`, `id_fleur`, `id_perso`, `id_bouquet`)
VALUES
(2, 'vide', 'dahlia', 'Bouquet01', 'vide'),
(4, 'Bulle fleurie', 'vide', 'Bouquet02', 'vide'),
(3, 'vide', 'tulipe', 'Bouquet03', 'vide'),
(2, 'Corde jute', 'vide', 'Bouquet04', 'vide'),
(1, 'vide', 'hortensia', 'vide', 'std1'),
(3, 'Carton carré', 'vide', 'vide', 'std2'),
(5, 'vide', 'lys', 'vide', 'std3'),
(4, 'vide', 'vide', 'vide', 'std4'),
(2, 'Grand vase', 'vide', 'Bouquet06', 'vide'),
(1, 'vide', 'violette', 'Bouquet07', 'vide'),
(1, 'vide', 'lila', 'Bouquet08', 'vide'),
(2,'vide','tulipe', 'Bouquet01','vide');



INSERT INTO `fleurs`.`magasin` (`id_magasin`,`nom_magasin`,`adresse_magasin`,`chiffre_affaires`) VALUES 
('MG01', 'Fleuriste du coin', '15 Rue des Lilas, 75020 Paris', 75000),
('MG02', 'Fleur de Lys', '12 Rue des Roses, 69001 Lyon', 52000),
('MG03', 'Jardin des Fleurs', '32 Avenue de la Paix, 13001 Marseille', 98000);


INSERT INTO `fleurs`.`stockfleur` (`id_fleur`,`id_magasin`,`quantite`)
VALUES 
       ('rose', 'MG01', 5),
       ('tulipe', 'MG01', 4),
       ('lys', 'MG01', 7),
       ('pivoine', 'MG01', 15),
       ('orchidée', 'MG01', 10),
       ('marguerite', 'MG01', 9),
       ('lila', 'MG01', 20),
       ('chrysanthème', 'MG01', 14),
       ('dahlia', 'MG01', 13),
       ('hortensia', 'MG01', 10),
       ('violette', 'MG01', 14),
       ('tournesol', 'MG01', 8),
       ('oeillet', 'MG01', 1),
       ('jonquille', 'MG01', 3),
       ('lavande', 'MG01',25),
       ('muguet', 'MG01',16),
       
        ('rose', 'MG02', 12),
       ('tulipe', 'MG02', 21),
       ('lys', 'MG02', 2),
       ('pivoine', 'MG02', 4),
       ('orchidée', 'MG02', 16),
       ('marguerite', 'MG02', 8),
       ('lila', 'MG02', 7),
       ('chrysanthème', 'MG02', 5),
       ('dahlia', 'MG02', 10),
       ('hortensia', 'MG02', 8),
       ('violette', 'MG02', 7),
       ('tournesol', 'MG02', 9),
       ('oeillet', 'MG02', 12),
       ('jonquille', 'MG02', 10),
       ('lavande', 'MG02',12),
       ('muguet', 'MG02',4),
       
        ('rose', 'MG03', 7),
       ('tulipe', 'MG03', 9),
       ('lys', 'MG03', 8),
       ('pivoine', 'MG03', 10),
       ('orchidée', 'MG03', 2),
       ('marguerite', 'MG03', 5),
       ('lila', 'MG03', 1),
       ('chrysanthème', 'MG03', 4),
       ('dahlia', 'MG03', 7),
       ('hortensia', 'MG03', 16),
       ('violette', 'MG03', 10),
       ('tournesol', 'MG03', 4),
       ('oeillet', 'MG03', 5),
       ('jonquille', 'MG03', 3),
       ('lavande', 'MG03',2),
       ('muguet', 'MG03',4);


INSERT INTO `fleurs`.`stockaccessoire` (`id_accessoire`,`id_magasin`,`quantite`) VALUES 
	
    ('Petit vase', 'MG01', 2),
    ('Corde jute', 'MG01', 1),
    ('Grand vase', 'MG01', 4),
    ('Bocal verre', 'MG01', 5),
    ('Cuivre laqué', 'MG01', 2),
    ('Bulle fleurie', 'MG01', 4),
    ('Carton carré', 'MG01', 2),
    ('Attrape rêve', 'MG01', 7),
    ('Composition chien', 'MG01', 5),
    ('Récipient zinc', 'MG01', 6),
    ('Pot','MG01',3),



	('Petit vase', 'MG02', 2),
    ('Corde jute', 'MG02', 7),
    ('Grand vase', 'MG02', 10),
    ('Bocal verre', 'MG02', 8),
    ('Cuivre laqué', 'MG02', 9),
    ('Bulle fleurie', 'MG02', 4),
    ('Carton carré', 'MG02', 14),
    ('Attrape rêve', 'MG02', 15),
    ('Composition chien', 'MG02', 1),
    ('Récipient zinc', 'MG02', 8),
    ('Pot','MG02',7),


	('Petit vase', 'MG03', 18),
    ('Corde jute', 'MG03', 15),
    ('Grand vase', 'MG03', 22),
    ('Bocal verre', 'MG03', 20),
    ('Cuivre laqué', 'MG03', 14),
    ('Bulle fleurie', 'MG03', 10),
    ('Carton carré', 'MG03', 12),
    ('Attrape rêve', 'MG03', 11),
    ('Composition chien', 'MG03', 18),
    ('Récipient zinc', 'MG03', 17),
    ('Pot','MG03',6);
    



INSERT INTO achat_dans (id_client, id_magasin) VALUES
('C1', 'MG01'),
('C2', 'MG02'),
('C3', 'MG03'),
('C4', 'MG01'),
('C5', 'MG02'),
('C6', 'MG03'),
('C7', 'MG01'),
('C8', 'MG01'),
('C9', 'MG02'),
('C10', 'MG02'),
('C11', 'MG02');


CREATE USER 'bozo'@'localhost' IDENTIFIED BY 'user';
GRANT SELECT, UPDATE ON fleurs.clients TO 'bozo'@'localhost';
Grant SELECT, INSERT on fleurs.commande to 'bozo'@'localhost';
Grant SELECT on fleurs.bouquet_std to 'bozo'@'localhost';
Grant SELECT on fleurs.bouquet_perso to 'bozo'@'localhost';
Grant SELECT on fleurs.composition to 'bozo'@'localhost';


