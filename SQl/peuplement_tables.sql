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



INSERT INTO `fleurs`.`bouquet_perso` (`id_perso`,`description_bouquet`,`prix_max`)
VALUES
('Bouquet1', 'Bouquet de roses rouges et blanches', 50.0),
('Bouquet2', 'Bouquet de pivoines et d''oeillets', 45.0),
('Bouquet3', 'Bouquet de tournesols et de gerberas', 40.0),
('Bouquet4', 'Bouquet de lys et de freesias', 55.0),
('Bouquet5', 'Bouquet de roses roses et d''oeillets', 35.0),
('Bouquet6', 'Bouquet de dahlias et de chrysanthèmes', 30.0),
('Bouquet7', 'Bouquet de renoncules et de marguerites', 38.0),
('Bouquet8', 'Bouquet de violettes et de campanules', 42.0),
('Bouquet9', 'Bouquet de pivoines et de roses blanches', 48.0),
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


INSERT INTO `fleurs`.`commande` (`num_commande`,`date_commande`,`adresse_livraison`,`message`,`date_livraison`,`etat_commande`,`id_client`,`id_perso`,`id_bouquet`)
VALUES
('1', '2023-04-28', '12 Rue de la Paix, Paris', 'Joyeux Anniversaire', '2023-05-30', 'VINV', 'C1', 'vide', 'std1'),
('2', '2023-04-29', '45 Rue de la Roquette, Lyon', 'Félicitations', '2023-05-20', 'CC', 'C2', 'vide', 'std10'),
('3', '2023-04-29', '5 Avenue des Champs-Élysées, Paris', 'Bonne Fête', '2023-05-15', 'CPAV', 'C3', 'Bouquet9', 'vide'),
('4', '2023-04-29', '28 Rue Sainte-Catherine, Bordeaux', 'vide', '2023-05-17', 'CAL', 'C4', 'Bouquet10', 'vide'),
('5', '2023-04-30', '9 Rue de la République, Marseille', 'Joyeuses Pâques', '2023-05-04', 'CL', 'C5', 'vide', 'std1'),
('6', '2023-04-30', '1 Place de la Comédie, Montpellier', 'vide', '2023-05-24', 'CAL', 'C6', 'Bouquet6', 'vide'),
('7', '2023-05-01', '16 Rue des Archives, Paris', 'Bonne Fête des Mères', '2023-05-26', 'CPAV', 'C7', 'Bouquet4','vide'),
('8', '2023-05-01', '78 Rue de la Pompe, Neuilly-sur-Seine', 'Joyeux Anniversaire', '2023-05-27', 'CC', 'C8', 'vide', 'std4'),
('9', '2023-05-01', '2 Rue de la République, Lyon', 'vide', '2023-05-30', 'VINV', 'C9', 'vide', 'std3'),
('10', '2023-05-02', '50 Rue de la République, Lille', 'Bonne Fête des Pères', '2023-05-03', 'CL', 'C9', 'vide', 'std1'),
('11', '2023-05-02', '10 Rue de la Convention, Marseille', 'vide', '2023-05-24', 'CAL', 'C11', 'vide', 'std5'),
('12','2002-01-15','15 avenue de serre, Paris','Vite svp','2002-01-20','CL','C10','Bouquet1','vide'),
('13','2020-01-15','15 avenue de serre, Paris','Allez','2020-01-20','CL','C10','Bouquet2','vide'),
('14','2021-01-20','15 avenue de serre, Paris','Yes','2021-01-25','CL','C10','Bouquet3','vide');





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

    
    
INSERT INTO `fleurs`.`fleur` (`id_fleur`,`couleur`,`prix_fleur`,`dispo_fleur`)
VALUES ('rose', 'rouge', 1.99, true),
       ('tulipe', 'jaune', 2.99, true),
       ('lys', 'blanc', 2.50, true),
       ('pivoine', 'rose', 1.75, false),
       ('orchidée', 'blanc', 3.25, true),
       ('marguerite', 'jaune', 1.99, true),
       ('lila', 'jaune', 0.99, false),
       ('chrysanthème', 'rose', 1.50, true),
       ('dahlia', 'rouge', 4.50, false),
       ('hortensia', 'bleu', 3.75, true),
       ('violette', 'violet', 2.25, true),
       ('tournesol', 'jaune', 2.99, true),
       ('oeillet', 'blanc', 3.50, true),
       ('jonquille', 'jaune', 4.50, true),
       ('lavande','violet',3,True),
       ('muguet','blanc',2,False),
       ('vide','vide',0,False);
   

       
       
INSERT INTO `fleurs`.`composition` (`prix_composition`, `id_accessoire`, `id_fleur`, `id_perso`, `id_bouquet`)
VALUES
(20, 'Bocal verre', 'dahlia', 'Bouquet1', 'vide'),
(15, 'Bulle fleurie', 'rose', 'Bouquet2', 'vide'),
(18, 'vide', 'tulipe', 'Bouquet3', 'vide'),
(22, 'Corde jute', 'vide', 'Bouquet4', 'vide'),
(25, 'vide', 'hortensia', 'vide', 'std1'),
(30, 'Carton carré', 'vide', 'vide', 'std2'),
(35, 'Récipient zinc', 'lys', 'vide', 'std3'),
(40, 'vide', 'vide', 'vide', 'std4'),
(20, 'Grand vase', 'tulipe', 'Bouquet6', 'vide'),
(15, 'Bocal verre', 'violette', 'Bouquet7', 'vide'),
(18, 'vide', 'lila', 'Bouquet8', 'vide'),
(20,'Pot','tulipe', 'Bouquet1','vide');



INSERT INTO `fleurs`.`magasin` (`id_magasin`,`nom_magasin`,`adresse_magasin`,`chiffre_affaires`) VALUES 
('MG01', 'Fleuriste du coin', '15 Rue des Lilas, 75020 Paris', 75000),
('MG02', 'Fleur de Lys', '12 Rue des Roses, 69001 Lyon', 52000),
('MG03', 'Jardin des Fleurs', '32 Avenue de la Paix, 13001 Marseille', 98000);






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
GRANT SELECT ON fleurs.clients TO 'bozo'@'localhost';
Grant insert on fleurs.commande to 'bozo'@'localhost';


