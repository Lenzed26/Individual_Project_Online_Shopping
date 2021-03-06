USE master;
go

DROP DATABASE IF EXISTS MonsterHunter;
go

CREATE DATABASE MonsterHunter;
go

USE MonsterHunter;
go

DROP TABLE IF EXISTS OrderDetails;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Hunters;
go

CREATE TABLE Hunters (
	HunterID int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(150) NOT NULL,
	Location varchar(100) NOT NULL,
);

CREATE TABLE Orders (
	OrderID int IDENTITY(1,1) PRIMARY KEY,
	HunterID int FOREIGN KEY REFERENCES Hunters(HunterID),
	OrderDate date NOT NULL,
	DeliveryDate date
);

CREATE TABLE Products (
	ProductID int IDENTITY(1,1) PRIMARY KEY,
	ProductName varchar(100) NOT NULL,
	UnitPrice decimal NOT NULL,
	Description varchar(150),
	Category varchar(30) NOT NULL,
	Rarity int NOT NULL
);

CREATE TABLE OrderDetails (
	OrderDetailsID int IDENTITY(1,1) PRIMARY KEY,
	OrderID int FOREIGN KEY REFERENCES Orders(OrderID),
	ProductID int FOREIGN KEY REFERENCES Products(ProductID),
	Quantity int NOT NULL,
	UnitPrice decimal NOT NULL
);

INSERT INTO Hunters 
VALUES
('Aiden', 'Astera'),
('Ace Lancer', 'Val Habar'),
('Ace Gunner', 'Bherna'),
('Argosy Captain', 'Moga Village'),
('Julius', 'Bherna'),
('Hinoa','Kamura Village'),
('Minoto', 'Kamura Village'),
('Fugen', 'Kamura Village'),
('Utsushi', 'Kamura Village'),
('Yomogi', 'Kamura Village'),
('Iori', 'Kamura Village')

INSERT INTO Products
VALUES
-- Product Name, Description, Category (i.e. Consumable/Weapon/Armour/Decoration), Rarity

--Consumables
('Herb', 1, 'A primary ingredient in potions', 'Consumable', 1),
('Antidote Herb', 1, 'A primary ingredient in antidotes', 'Consumable', 1),
('Nullberry', 1200, 'Mysterious berry that cures blights and resistance down', 'Consumable', 2),
('Potion', 66, 'Resotres a small amount of health', 'Consumable', 1),
('Mega Potion', 165, 'Restores a moderate amount of health', 'Consumable', 2),
('Life Powder', 480, 'Produces a cloud that heals you and anyone nearby','Consumable',4),
('Dust of Life', 4000, 'Produces a cloud that restores a large amount of health for you and anyone nearby','Consumable', 4),
('Antidote', 60, 'A cure for poison', 'Consumable', 1),
('Herbal Medicine', 250, 'Removes all traces of poison and restores a slight amount of health', 'Consumable', 2),
('Herbal Powder', 6300,'Produces a cloud that cures poison and heals you a bit', 'Consumable', 4),
('Ration', 30,'Food that restores a bit of stamina','Consumable', 1),
('Max Potion', 1700,'Fully restores health', 'Consumable', 3),
('Ancient Potion', 2450, 'Fully restores your health and stamina', 'Consumable', 4),
('Energy Drink', 60,'A Guild-approved beverage that boosts your stamina. Helps shake off sleep, too!', 'Consumable', 2),
('Cleanser', 140,'Immediately removes any foam or webbing on your body', 'Consumable', 2),
('Deodorant', 80,'An item that cures Stench, hellfireblight, and blastblight when thrown down', 'Consumable', 2),
('Adamant Seed', 220, 'Temporarily raises your defense when ingested by hardening tissue', 'Consumable', 2),
('Armorskin', 578, 'Boosts your defense by turning your skin as hard as rock', 'Consumable', 4),
('Mega Armorskin', 2696, 'Boosts your defense even more than a regular armorskin', 'Consumable', 5),
('Hardshell Powder', 6300, 'A mysterious powder; when dispersed, temporarily strengthens defense', 'Consumable', 4),
('Might Seed', 280, 'Temporarily strengthens your attacks when ingested by improving energy flow', 'Consumable', 2),
('Demondrug', 668, 'Boosts your attack power by filling you with - guess what? - demonic strength', 'Consumable', 4),
('Mega Demondrug', 2831, 'Boosts your attack power even more than a regular Demondrug', 'Consumable', 5),
('Demon Powder', 6300, 'A mysterious powder; when dispersed, temporarily strengthens attacks', 'Consumable', 4),
('Farcaster', 150, 'Instantly transports you to the nearest camp. Can also be used during battles', 'Consumable', 3),
('Poison Smoke Bomb', 600, 'Releases a toxic cloud of mist when used. Also popular as a household bug bomb', 'Consumable', 2),
('Flash Bomb', 572, 'Flashes brightly on impact. Toss this right under a monster''s nose to blind it', 'Consumable', 2),
('Sonic Bomb', 450, 'A hand-thrown bomb that emits a high-frequency blast of sound on detonation', 'Consumable', 2),
('Dung Bomb', 120, 'A particularly odorous ball that causes monsters to flee', 'Consumable', 2),
('Tranq Bomb', 180, 'A bomb used to capture monsters ensnared in traps. Can be used in various ways', 'Consumable', 3),
('Raw Meat', 50, 'Meat carved from a monster. Can be cooked, combined, or used to set a trap', 'Consumable', 1),
('Burnt Meat', 10,'Meat that has been burnt to a crisp. It might boost your Stamina, or it might drain it...', 'Consumable', 1),
('Rare Steak', 30,'Provides a moderate boost to your stamina. This one''s still red in the middle', 'Consumable', 1),
('Well-done Steak', 96,'Food that fully restores stamina', 'Consumable', 2),
('Immunizer', 600, 'Boosts your natural ability to heal', 'Consumable', 2),
('Dash Juice', 293, 'Reduces stamina depletion for a period of time as well as restoring a bit of stamina', 'Consumable', 2),
('Poisoned Meat', 188, 'Raw meat that poisons whatever eats it. Makes great bait for traps', 'Consumable', 2),
('Tinged Meat', 300, 'Raw meat that paralyzes whatever eats it. Makes great bait for traps', 'Consumable', 2),
('Drugged Meat', 300, 'Raw meat that puts whatever eats it to sleep. Makes great bait for traps', 'Consumable', 2),
('Pitfall Trap', 400, 'A trap for catching certain large monsters, It is tripped by a heavy weight', 'Consumable', 3),
('Shock Trap', 320, 'A trap that imobilizes a target. Use it to capture monsters', 'Consumable', 3),

--Ammo
('Normal Ammo 1', 1, 'Ammo for novices. Doesn''t pack much oomph. Unlimited shots', 'Ammo', 4),
('Normal Ammo 2', 3, 'Non-specialized ammo. More powerful than Normal Ammo 1', 'Ammo', 2),
('Normal Ammo 3', 5, 'High-quality gunpowder ammo. Single-shot, but fast and impactful', 'Ammo', 3),
('Pierce Ammo 1', 4, 'Armor-piercing ammo that deals multiple hits to some monsters', 'Ammo', 1),
('Pierce Ammo 2', 8, 'High-grade armor-piercing ammo. Deals more hits than Pierce Ammo 1', 'Ammo', 2),
('Pierce Ammo 3', 12, 'Max-grade armor-piercing ammo. Deals numerous hits', 'Ammo', 3),
('Spread Ammo 1', 4, 'Ammo made for close combat. What it lacks in range, it makes up for in damage', 'Ammo', 1),
('Spread Ammo 2', 8, 'Ammo made for close combat. Deals more hits than Spread Ammo 1', 'Ammo', 2),
('Spread Ammo 3', 12, 'Max-grade ammo made for close combat. Deals numerous hits', 'Ammo', 3),
('Shrapnel Ammo 1', 3, 'Ammo that covers a wide area. Who needs accuracy when you hit so much?', 'Ammo', 1),
('Shrapnel Ammo 2', 6, 'Ammo that covers a wide area. More range and even more hits than Shrapnel Ammo 1', 'Ammo', 2),
('Shrapnel Ammo 3', 9, 'Max-grade shrapnel ammo that delivers maximum range and hit-count over a wide area', 'Ammo', 3),
('Cluster Bomb 1', 30, 'Ammo that fragments into three shells upon impact. Beware of friendly fire!', 'Ammo', 2),
('Cluster Bomb 2', 60, 'Ammo that framents into four shells upon impact. Beware of friendly fire!', 'Ammo', 3),
('Cluster Bomb 3', 90, 'Ammo that fragments into five shells upon impact. Beware of friendly fire!', 'Ammo', 4),
('Sticky Ammo 1', 12, 'Powerful pierce and burst ammo. Headshots may stun', 'Ammo', 1),
('Sticky Ammo 2', 24, 'Very powerful pierce and burst ammo. Headshots may stun', 'Ammo', 2),
('Sticky Ammo 3', 36, 'Powerful pierce and burst ammo. Headshots stun', 'Ammo', 3),
('Power Coating', 20, 'An arrow coating that increases the attack power of arrows', 'Ammo', 2),
('Blast Coating', 20, 'An arrow coating that applies explosive powder to arrows', 'Ammo', 3),
('Exhaust Coating', 21, 'An arrow coating that applies a fatigue-inducing fluid to arrows', 'Ammo', 3),
('Para Coating', 26, 'An arrow coating that applies paralysis effects to arrows', 'Ammo', 3),
('Poison Coating', 17, 'An arrow coating that applies poison effects to arrows', 'Ammo', 3),
('Sleep Coating', 17, 'An arrow coating that applies sleep effects to arrows', 'Ammo', 3),


--Bombs
('Barrel Bomb', 156, 'A small time bomb', 'Bomb', 2),
('Large Barrel Bomb', 518, 'A powerful bomb triggered by external physical impact', 'Bomb', 3),
('Mega Barrel Bomb', 800, 'An upgraded large barrel bomb. EFfective against large monsters', 'Bomb', 4),

--Weapons
('Kamura Cleaver I', 500, 'A high-quality great sword forges using world-famous steel from Kamura Village', 'Weapon', 1),
('Kamura Sword I', 300, 'This Kamura special delivers the essentials - sharp edge and sturdy shield', 'Weapon', 1),
('Kamura Glintblades I', 300, 'Extremely sharp dual blades from Kamura Village. Require great care to use safely', 'Weapon', 1),
('Kamura Blade I', 500, 'A Kamura-crafted long sword made of fine steel. A simple but high-quality weapon', 'Weapon', 1),
('Kamura Hammer I', 300, 'A hammer crafted in Kamura Village. It''s somewhat gimmicky but still strong', 'Weapon', 1),
('Kamura Chorus I', 300, 'A Kamura-made hunting horn that uses blades to produce a chiming chorus', 'Weapon', 1),
('Kamura Spear I', 300, 'A Kamura-crafted spear. Despite it''s appearance, it''s pretty easy to handle', 'Weapon', 1),
('Kamura Gunlance I', 300, 'Made in Kamura Village, this gunlance is famous for its elegant and refined design', 'Weapon', 1),
('Kamura Iron Axe I', 300, 'A switch axe forged with impeccable Kamura technique, giving it its hi-tech specs', 'Weapon', 1),
('Kamura C. Blade I', 300, 'A charge blade made to meet the villagers'' needs. Looks bold, and packs a punch too', 'Weapon', 1),
('Kamura Glaive I', 300, 'An insect glaive forged in Kamura fires and fitted with blades of oxidized silver', 'Weapon', 1),
('Kamura Iron Bow I', 300, 'A popular bow around Kamura that has an easy draw and holds up to high tension', 'Weapon', 1),
('Kamura L.Bowgun I', 300, 'Made by the bowgun expert, Hamon. A masterful piece made from wood and steel', 'Weapon', 1),
('Kamura H.Bowgun I', 300, 'Braced with steel for peak devastation, this bowgun is a Kamura Special', 'Weapon', 1),

--Armour
('Kamura Head Scarf', 100, 'Signature Kamura Headwear. All village defenders sport this dark blue attire', 'Armour', 1),
('Kamura Garb', 100, 'A Kamura staple. Comes with shuriken to show off to your friends!', 'Armour', 1),
('Kamura Braces', 100, 'Signature Kamura armguards. Bound with red cord, they''re very lightweight', 'Armour', 1),
('Kamura Obi', 100, 'A sash that can conceal all manner of hunting tools via a no-nonsense design', 'Armour', 1),
('Kamura Leggings', 100, 'Leg armor that fits the user like a glove...for the foot. Made of local materials', 'Armour', 1),
('Leather Headgear', 100, 'Headgear made from tanned monster hide. Sturdy and suitable for anyone to use', 'Armour', 1),
('Leather Vest', 100, 'Chest armor made from leather and other light, easily obtainable materials', 'Armour', 1),
('Leather Gloves', 100, 'Gloves crafted from leather and other light materials. Made to be easy to use', 'Armour', 1),
('Leahter Belt', 100, 'A belt made from leather and other light materials. Basic general equipment', 'Armour', 1),
('Leather Pants', 100, 'Leg armor preferred as basic hunter equipment. Light and easy to move in', 'Armour', 1),
('Chainmail Headgear', 200, 'Sturdy head armor made from quality ore. Reliable mail relied on by many a hunter!', 'Armour', 1),
('Chainmail Vest', 200, 'Sturdy chest armor made of chainmail. Responsible for saving many a hunter''s life', 'Armour', 1),
('Chainmail Gloves', 200, 'Sturdy gloves made of chainmail. Popular and easy to maintain', 'Armour', 1),
('Chainmail Belt', 200, 'Sturdy waist armor crafted with a special process. Thin and light on the hips', 'Armour', 1),
('Chainmail Pants', 200, 'Sturdy leg armor. Made with as little metal as possible, so as to be easy to wear', 'Armour', 1),

--Decorations
('Wall Run Jewel 2', 500, 'A decoration that enhances the Wall Runner skill', 'Decoration', 4),
('Wirebug Jewel 2', 500, 'A decoration that enhances the Wirebug Whisperer skill', 'Decoration', 4),
('Enduring Jewel 2', 500, 'A decoration that enhances the Item Prolonger skill', 'Decoration', 4),
('Recovery Jewel 1', 500, 'A decoration that enhances the Recovery Speed skill', 'Decoration', 4),
('Medicine Jewel 2', 500, 'A decoration that enhances the Recovery Up skill', 'Decoration', 4),
('Sonorous Jewel 1', 500, 'A decoration that enhances the Horn Maestro skill', 'Decoration', 4),
('Drain Jewel 1', 500, 'A decoration that enhances the Stamina Thief skill', 'Decoration', 4)