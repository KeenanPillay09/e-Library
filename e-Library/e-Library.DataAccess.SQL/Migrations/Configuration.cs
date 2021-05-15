namespace e_Library.DataAccess.SQL.Migrations
{
    using e_Library.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<e_Library.DataAccess.SQL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(e_Library.DataAccess.SQL.DataContext context)
        {
            //Books
            var books = new List<Book>
            {
                
                //Fiction & Literature (11 books)
                new Book { Name = "Macbeth", Author ="William Shakespeare",
                    Description = "The authoritative edition of Macbeth from The Folger Shakespeare Library, the trusted and widely used Shakespeare series for students and general readers",
                    Genre ="Fiction & Literature",Price = 80,Stock = 10 },

                new Book { Name = "Othello", Author ="William Shakespeare",
                    Description = "In Othello, Shakespeare creates powerful drama from a marriage between the exotic Moor Othello and the Venetian lady Desdemona that begins with elopement and mutual devotion and ends with jealous rage and death",
                    Genre ="Fiction & Literature",Price = 80,Stock = 10 },

                new Book { Name = "Coriolanus", Author ="William Shakespeare",
                    Description = "Set in the earliest days of the Roman Republic, Coriolanus begins with the common people, or plebeians, in armed revolt against the patricians. The people win the right to be represented by tribunes. Meanwhile, there are foreign enemies near the gates of Rome",
                    Genre ="Fiction & Literature",Price = 70,Stock = 2 },

                new Book { Name = "Antony and Cleopatra", Author ="William Shakespeare",
                    Description = "Antony and Cleopatra dramatizes a major event in world history: the founding of the Roman Empire. The future first emperor, Octavius Caesar (later called Augustus Caesar), cold-bloodedly manipulates other characters and exercises iron control over himself.",
                    Genre ="Fiction & Literature",Price = 70,Stock = 3 },

                new Book { Name = "Hamlet", Author ="William Shakespeare",
                    Description = "Hamlet is Shakespeare’s most popular, and most puzzling, play. It follows the form of a “revenge tragedy,” in which the hero, Hamlet, seeks vengeance against his father’s murderer, his uncle Claudius, now the king of Denmark. Much of its fascination, however, lies in its uncertainties.",
                    Genre ="Fiction & Literature",Price = 80,Stock = 5 },

                new Book { Name = "Romeo and Juliet", Author ="William Shakespeare",
                    Description = "In Romeo and Juliet, Shakespeare creates a violent world, in which two young people fall in love. It is not simply that their families disapprove; the Montagues and the Capulets are engaged in a blood feud.",
                    Genre ="Fiction & Literature",Price = 80,Stock = 10 },

                new Book { Name = "The Tempest", Author ="William Shakespeare",
                    Description = "Putting romance onstage, The Tempest gives us a magician, Prospero, a former duke of Milan who was displaced by his treacherous brother, Antonio. Prospero is exiled on an island, where his only companions are his daughter, Miranda, the spirit Ariel, and the monster Caliban. When his enemies are among those caught in a storm near the island, Prospero turns his power upon them through Ariel and other spirits.",
                    Genre ="Fiction & Literature",Price = 80,Stock = 6 },

                new Book { Name = "Twelfth Night", Author ="William Shakespeare",
                    Description = "Named for the twelfth night after Christmas, the end of the Christmas season, Twelfth Night plays with love and power. The Countess Olivia, a woman with her own household, attracts Duke (or Count) Orsino. Two other would-be suitors are her pretentious steward, Malvolio, and Sir Andrew Aguecheek.",
                    Genre ="Fiction & Literature",Price = 80,Stock = 4 },

                new Book { Name = "Julius Caesar", Author ="William Shakespeare",
                    Description = "Shakespeare may have written Julius Caesar as the first of his plays to be performed at the Globe, in 1599. For it, he turned to a key event in Roman history: Caesar’s death at the hands of friends and fellow politicians. Renaissance writers disagreed over the assassination, seeing Brutus, a leading conspirator, as either hero or villain. Shakespeare’s play keeps this debate alive.",
                    Genre ="Fiction & Literature",Price = 80,Stock = 5 },

                new Book { Name = "The Taming of the Shrew", Author ="William Shakespeare",
                    Description = "Love and marriage are the concerns of Shakespeare’s The Taming of the Shrew. Lucentio’s marriage to Bianca is prompted by his idealized love of an apparently ideal woman. Petruchio’s wooing of Katherine, however, is free of idealism. Petruchio takes money from Bianca’s suitors to woo her, since Katherine must marry before her sister by her father’s decree; he also arranges the dowry with her father. Petruchio is then ready to marry Katherine, even against her will.",
                    Genre ="Fiction & Literature",Price = 80,Stock = 3 },

                new Book { Name = "A Midsummer Night's Dream", Author ="William Shakespeare",
                    Description = "In A Midsummer Night’s Dream, Shakespeare stages the workings of love. Theseus and Hippolyta, about to marry, are figures from mythology. In the woods outside Theseus’ Athens, two young men and two young women sort themselves out into couples—but not before they form first one love triangle, and then another.",
                    Genre ="Fiction & Literature",Price = 80,Stock = 4 },


                //Childrens Books
                new Book { Name = "Diary Of A Wimpy Kid", Author ="Jeff Kinney",
                    Description = "The first in Jeff Kinney's side-splitting series, join Greg Heffley as he's thrust into a new year, and a new school, where undersize weaklings share the corridors with kids who are taller, meaner and already shaving.",
                    Genre ="Children's Books",Price = 195,Stock = 7 },

                new Book { Name = "Diary of a Wimpy Kid: Dog Days", Author ="Jeff Kinney",
                    Description = "It's the summer, and we're back with Greg Heffley and his crazy family in the fourth mega-selling instalment of Jeff Kinney's hilarious Diary of a Wimpy Kid series!  The way I like to spend my summer holidays is in front of the TV, playing video games with the curtains closed and the light turned off.  Unfortunately, Mom's idea of the perfect summer holiday is different from mine.",
                    Genre ="Children's Books",Price = 200,Stock = 2 },

                new Book { Name = "Diary of a Wimpy Kid: Double Down", Author ="Jeff Kinney",
                    Description = "It's the eleventh book in the bestselling Diary of a Wimpy Kid series!  The pressure's really piling up on Greg Heffley. His mom thinks video games are turning his brain to mush, so she wants her son to put down the controller and explore his 'creative side'.  As if that's not scary enough, Halloween's just around the corner and the frights are coming at Greg from every angle",
                    Genre ="Children's Books",Price = 220,Stock = 4 },

                new Book { Name = "Diary of a Wimpy Kid: The Last Straw", Author ="Jeff Kinney",
                    Description = "It's the third instalment of Jeff Kinney's award winning Diary of a Wimpy Kid series, but will it be third time lucky for our hero, Greg Heffley?  It's not easy for me to think of ways to improve myself, because I'm pretty much one of the best people I know.",
                    Genre ="Children's Books",Price = 195,Stock = 4 },

                new Book { Name = "Diary of a Wimpy Kid: Cabin Fever", Author ="Jeff Kinney",
                    Description = "It's book six of Jeff Kinney's award winning, bestselling Diary of a Wimpy Kid series, and life isn't getting any easier for Greg Heffley!  Over the past few days we've been running low on food, and if the snow doesn't melt quick I don't know WHAT we're gonna do.",
                    Genre ="Children's Books",Price = 210,Stock = 3 },

                new Book { Name = "Diary of a Wimpy Kid: The Ugly Truth", Author ="Jeff Kinney",
                    Description = "Dive into Jeff Kinney's fifth instalment of his rib-tickling and bestselling Diary of a Wimpy Kid series!  I'm in the market for a new best friend. The problem is, I invested all my time in Rowley, and I don't have anyone lined up to take his place.",
                    Genre ="Children's Books",Price = 200,Stock = 3 },

                new Book { Name = "Diary of a Wimpy Kid: The Long Haul", Author ="Jeff Kinney",
                    Description = "Jeff Kinney has brought Greg Heffley back for the ninth instalment of his bestselling Diary of a Wimpy Kid series.  This time, the Heffleys are off on a road trip! The chances of survival are... quite small to be honest.",
                    Genre ="Children's Books",Price = 220,Stock = 4 },

                new Book { Name = "Diary of a Wimpy Kid: Old School", Author ="Jeff Kinney",
                    Description = "In the tenth book, more bad luck follows Greg Heffley in Jeff Kinney's laugh out loud series!  I really don't understand why Mom thinks we need to go BACKWARDS, anyway. From what I can tell, the old days weren't that much fun.",
                    Genre ="Children's Books",Price = 220,Stock = 5 },

                new Book { Name = "Diary of a Wimpy Kid: Hard Luck", Author ="Jeff Kinney",
                    Description = "Everyone's favourite wimpy kid is back for his eighth appearance in Jeff Kinney's world-famous series.  Get stuck in to Diary of a Wimpy Kid: Hard Luck to find out what scrapes Greg Heffley has gotten himself into this time!",
                    Genre ="Children's Books",Price = 210,Stock = 5 },

                new Book { Name = "Diary of a Wimpy Kid: The Getaway", Author ="Jeff Kinney",
                    Description = "Book 12 is the best yet in the brilliant, bestselling Diary of a Wimpy Kid series!  Greg Heffley and his family are getting out of town.  With the cold weather setting in and the stress of the Christmas holiday approaching, the Heffleys decide to escape to a tropical island resort for some much-needed rest and relaxation. A few days in paradise should do wonders for Greg and his frazzled family.",
                    Genre ="Children's Books",Price = 250,Stock = 7 },

                new Book { Name = "Diary of a Wimpy Kid: Rodrick Rules", Author ="Jeff Kinney",
                    Description = "In the second book of Jeff Kinney's bestselling series, team up with Greg Heffley once again, this time to try and take on his big brother. As you can guess, it doesn't work out too well...  Rodrick actually got a hold of my LAST journal a few weeks back, and it was a disaster. But don't even get me started on THAT story.",
                    Genre ="Children's Books",Price = 210,Stock = 3 },

                //Crime & Mystery
                new Book { Name = "Mad Dogs", Author ="Robert Muchamore",
                    Description = "The British underworld is controlled by gangs. When two of them start a turf war, violence explodes on to the streets. The police need information fast, and James Adams has the contacts to infiltrate the most dangerous gang of all. He works for CHERUB.",
                    Genre ="Crime & Mystery",Price = 167,Stock = 7 },

                new Book { Name = "Maximum Security", Author ="Robert Muchamore",
                    Description = "Under American law, kids convicted of serious crimes can be sentenced as adults. Two hundred and eighty of these child criminals live in the sunbaked desert prison of Arizona Max. In one of the most dangerous CHERUB missions ever, James Adams has to go undercover inside Arizona Max, befriend an inmate and then bust him out.",
                    Genre ="Crime & Mystery",Price = 167,Stock = 5 },

                new Book { Name = "The General", Author ="Robert Muchamore",
                    Description = "The world's largest urban warfare training compound stands in the desert near Las Vegas. Forty British commandos are being hunted by an entire American battalion.But their commander has an ace up his sleeve: he plans to smuggle in ten CHERUB agents, and fight the best war game ever.",
                    Genre ="Crime & Mystery",Price = 167,Stock = 4 },

                new Book { Name = "The Sleepwalker", Author ="Robert Muchamore",
                    Description = "An airliner explodes over the Atlantic leaving 345 people dead. Crash investigators suspect terrorism, but they're getting nowhere.A distressed twelve-year-old calls a police hotline and blames his father for the explosion. It could be a breakthrough, but there's no hard evidence and the boy has a history of violence and emotional problems.",
                    Genre ="Crime & Mystery",Price = 170,Stock = 6 },

                new Book { Name = "Man vs Beast", Author ="Robert Muchamore",
                    Description = "Every day thousands of animals die in laboratory experiments. Some say these experiments provide essential scientific knowledge, while others will do anything to prevent them.",
                    Genre ="Crime & Mystery",Price = 165,Stock = 3 },

                new Book { Name = "Class A", Author ="Robert Muchamore",
                    Description = "James Adams is on his biggest mission yet, working to nail Europe's most powerful cocaine dealer. He'll need all his specialist training if he's going to bring down the man at the top.",
                    Genre ="Crime & Mystery",Price = 170,Stock = 5 },

                new Book { Name = "The Recruit", Author ="Robert Muchamore",
                    Description = "The first title in the number one bestselling CHERUB series! James hits rock bottom before he's offered a new start in an intriguing organisation ...",
                    Genre ="Crime & Mystery",Price = 167,Stock = 3 },

                new Book { Name = "Shadow Wave", Author ="Robert Muchamore",
                    Description = "After a tsunami causes massive devastation to a tropical island, its governor sends in the bulldozers to knock down villages, replacing them with luxury hotels.Guarding the corrupt governor's family isn't James Adams' idea of the perfect mission, especially as it's going to be his last as a CHERUB agent. And then retired colleague Kyle Blueman comes up with an unofficial and highly dangerous plan of his own.",
                    Genre ="Crime & Mystery",Price = 170,Stock = 3 },

                new Book { Name = "The Killing", Author ="Robert Muchamore",
                    Description = "Leon is a small-time crook who's ridden his luck for three decades. When he starts splashing big money around, the cops are desperate to know where it came from",
                    Genre ="Crime & Mystery",Price = 170,Stock = 2 },

                new Book { Name = "Brigands M.C.", Author ="Robert Muchamore",
                    Description = "Every CHERUB agent comes from somewhere. Dante Scott still has nightmares about the death of his family, brutally murdered by a biker gang.Dante is given the chance to become a member of CHERUB, a trained professional with one essential advantage: adults never suspect that children are spying on them.",
                    Genre ="Crime & Mystery",Price = 175,Stock = 7 },

                new Book { Name = "Divine Madness", Author ="Robert Muchamore",
                    Description = "When CHERUB uncovers a link between eco-terrorist group Help Earth and a wealthy religious cult known as The Survivors, James Adams is sent to Australia on an infiltration mission.",
                    Genre ="Crime & Mystery",Price = 165,Stock = 3 },

                new Book { Name = "The Fall", Author ="Robert Muchamore",
                    Description = "When an MI5 operation goes disastrously wrong, James Adams needs all of his skills to get out of Russia alive.",
                    Genre ="Crime & Mystery",Price = 170,Stock = 2 },

                new Book { Name = "Dark Sun", Author ="Robert Muchamore",
                    Description = "It's the last day of term. Three lads are clearing out their lockers, organising a sleepover and hatching a plan to spatter a girl with rancid coleslaw.But things aren't what they seem. One boy's father is a member of Dark Sun, a criminal organisation dealing in nuclear weapons technology, while another is a CHERUB agent sent to stop him.",
                    Genre ="Crime & Mystery",Price = 60,Stock = 5 },
            };

            //Authors
            var authors = new List<BookAuthor>
            { 
                new BookAuthor {Author ="William Shakespeare"},
                new BookAuthor {Author ="Jeff Kinney"},
                new BookAuthor {Author ="Robert Muchamore"},
            };

            //Genre
            var genres = new List<BookGenre>
            {
                new BookGenre {Genre ="Biographies & Memoirs"},
                new BookGenre {Genre ="Health, Mind & Body"},
                new BookGenre {Genre ="Romance"},
                new BookGenre {Genre ="Children's Books"},
                new BookGenre {Genre ="Fantasy & Science Fiction"},
                new BookGenre {Genre ="Arts & Entertainment"},
                new BookGenre {Genre ="Religion & Spirituality"},
                new BookGenre {Genre ="Thriller & Horror"},
                new BookGenre {Genre ="Computers & Technology"},
                new BookGenre {Genre ="Crime & Mystery"},
                new BookGenre {Genre ="Fiction & Literature"},
            };
        }
    }
}
//new Book { Name = "", Author ="",
//Description = "",
//Genre ="",Price = 0,Stock = 0 },