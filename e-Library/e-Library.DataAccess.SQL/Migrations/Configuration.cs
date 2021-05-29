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
            genres.ForEach(g => context.BookGenres.AddOrUpdate(c => c.Genre, g));
            context.SaveChanges();

            //Authors
            var authors = new List<BookAuthor>
            {
            new BookAuthor {Author ="Ryan Blumenthal"},
            new BookAuthor {Author ="Mosilo Mothepu"},
            new BookAuthor {Author ="Raynor Winn"},
            new BookAuthor {Author ="Ben Macintyre"},
            new BookAuthor {Author ="Denise Fergus"},
            new BookAuthor {Author ="Bill Bryson"},
            new BookAuthor {Author ="Danielle Steel"},
            new BookAuthor {Author ="AI J Venter"},
            new BookAuthor {Author ="Robin Sharma"},
            new BookAuthor {Author ="James Clear"},
            new BookAuthor {Author ="Odette C Bell"},
            new BookAuthor {Author ="Hayley McGregor"},
            new BookAuthor {Author ="Holly Whitaker"},
            new BookAuthor {Author ="William Shakespeare"},
            new BookAuthor {Author ="Clare Pooley"},
            new BookAuthor {Author ="Oprah Winfrey"},
            new BookAuthor {Author ="Jeff Kinney"},
            new BookAuthor {Author ="Robert Muchamore"},
            new BookAuthor {Author ="Ebbe Dommisse"},
            new BookAuthor {Author ="Glennon Doyle"},
            new BookAuthor {Author ="Mathew McConaughey"},
            new BookAuthor {Author ="James Patterson"},
            new BookAuthor {Author ="Stephen Walther"},
            };
            authors.ForEach(a => context.BookAuthors.AddOrUpdate(c => c.Author, a));
            context.SaveChanges();


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


                //Childrens Books (11 books)
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

                //Crime & Mystery (13 books)
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

             //Romance (12 books)
                new Book {Name = "A Perfect Life",Author = "Danielle Steel",
                Description = "An icon in the world of television news, Blaise McCarthy seems to have it all: beauty, intelligence and courage. But privately there is a story she has protected for years . . .",
                Genre = "Romance",Price = 110,Stock = 6},

                new Book { Name = "Blue", Author ="Danielle Steel",
                Description = "Ginny Carter was once a rising star in TV news, married with a young son – until her whole world dissolved on a freeway in a single instant. In the aftermath, she somehow pieces her life back together, but struggles to truly find meaning in her life.",
                Genre ="Romance",Price =110 ,Stock = 3 },

                new Book { Name = "Daddy", Author ="Danielle Steel",
                Description = "To the outside world, Sarah and Oliver Watson had the perfect marriage. Happy and successful, with three beautiful children, they seemed to have it all. But under the surface, Sarah felt lost, empty and inadequate. And one Christmas, after eighteen years of marriage, she walked out.",
                Genre ="Romance",Price =110 ,Stock = 4 },

                new Book { Name = "Dangerous Games", Author ="Danielle Steel",
                Description = "Television correspondent Alix Phillips dodges bullets and breaks rules to bring the most important news to the world. Working alongside cameraman Ben Chapman, a deeply private ex Navy SEAL, Alix revels in the risks and whirlwind pace of her work",
                Genre ="Romance",Price =179,Stock = 0 },

                new Book { Name = "His Bright Light", Author ="Danielle Steel",
                Description = "This is the story of an extraordinary boy with a brilliant mind, a heart of gold, and a tortured soul. It is the story of an illness, a fight to live, and a race against death",
                Genre ="Romance",Price =189 ,Stock = 0 },

                new Book { Name = "Journey", Author ="Danielle Steel",
                Description = "Everyone in Washington knows Madeleine and Jack Hunter. Maddy is an award-winning TV anchorwoman and Jack is the head of her network. To the world, theirs is a storybook marriage.",
                Genre ="Romance",Price =123 ,Stock = 0 },

                new Book { Name = "Magic", Author ="Danielle Steel",
                Description = "It starts on a summer evening, with the kind of magic found only in Paris. Once a year in the City of Light, a lavish dinner takes place outside a spectacular landmark.",
                Genre ="Romance",Price =137 ,Stock = 0 },

                new Book { Name = "Malice", Author ="Danielle Steel",
                Description = "At seventeen, on the night of her mother's funeral, Grace Adams is attacked. A young woman with secrets too horrible to tell, with hurts so deep they may never heal, Grace will not tell the truth about the attack. She is beautiful enough for men to want her, but after a lifetime of being a victim, now she must pay the price for other people's sins.",
                Genre ="Romance",Price =123,Stock = 0 },

                new Book { Name = "The Ghost", Author ="Danielle Steel",
                Description = "With a wife he loves and an exciting London-based career, architect Charles Waterston's life seems in perfect balance. Nothing prepares him for the sudden end to his ten-year marriage-or his unwanted transfer to his firm's New York office.",
                Genre ="Romance",Price = 189,Stock = 0 },

                new Book { Name = "The Gift", Author ="Danielle Steel",
                Description = "On a June day, a young woman in a summer dress steps off a Chicago-bound bus into a small midwestern town. She doesn't intend to stay. She is just passing through. Yet her stopping here has a reason and it is part of a story that you will never forget.",
                Genre ="Romance",Price =189 ,Stock = 0 },

                new Book { Name = "The Kiss", Author ="Danielle Steel",
                Description = "In her 53rd bestselling novel, Danielle Steel explores how a single shattering moment can change lives forever. The Kiss is at once a moving testament to the fragility of life and a breathtaking story about the power of love to heal, to free, to transform, and to make broken spirits whole.",
                Genre ="Romance",Price =309 ,Stock = 0 },

                new Book { Name = "The Long Road Home", Author ="Danielle Steel",
                Description = "A novel of courage, hope and love..From her secret perch at the top of the stairs, seven-year-old Gabriella watches the guests arrive at her parents' lavish Manhattan home. The click, click click of her mother's high heels strikes terror into her heart, as she has been told that she is to blame for her mother's rage - and her father's failure to protect her.",
                Genre ="Romance",Price =123 ,Stock = 0 },

                //Fantasy & Science Fiction (8 books)
                new Book { Name = "Angel", Author ="Odette C Bell",
                Description = "Right and wrong will cost you..Lizzie Luck is magical. Apparently. The DNA test came back proving she's from the otherworld.She's unemployed, has 24 dollars in her account, and is so out of luck it's killing her.Things couldn't get worse, right?",
                Genre ="Science Fiction & Fantasy",Price = 83,Stock = 5 },

                new Book { Name = "Ava", Author ="Odette C Bell",
                Description = "The Force are coming, and this time, there may be little the Coalition can do to stop them.They’ve hatched a plan to destabilize the Artaxan Protectorate - one of the most powerful sovereign states of the Milky Way. The Artaxans claim they hold the key to history. Information specialists, their empire is built on trading and storing data. The Coalition wouldn't run without them. But now, the Artaxans are in turmoil.",
                Genre ="Science Fiction & Fantasy",Price = 70,Stock = 4 },

                new Book { Name = "Ki", Author ="Odette C Bell",
                Description = "In a snapped second his life changes. A woman falls from the sky, right before his eyes. She is his enemy. Yet the men that hunt her are worse. As his life crashes down around him, Jackson is faced with a horrible choice: blind loyalty to the country he loves of informed betrayal for a woman he hardly knows. He chooses her.",
                Genre ="Science Fiction & Fantasy",Price = 67,Stock = 2 },

                new Book { Name = "Magic Born", Author ="Odette C Bell",
                Description = "Monique is a broken witch on the run. Three years ago, she escaped being sacrificed at Vendex Academy for Magic.She’s been running since. Until now. ",
                Genre ="Science Fiction & Fantasy",Price = 70,Stock = 8 },

                new Book { Name = "Star Soldier", Author ="Odette C Bell",
                Description = "All must fight to live. But when a creature with near limitless power choses her to save her world, Ami is swept into a fight for everything and everyone.",
                Genre ="Science Fiction & Fantasy",Price = 135,Stock = 3 },

                new Book { Name = "Frozen Witch", Author ="Odette C Bell",
                Description = " A world of magic, of crime, of retribution.",
                Genre ="Science Fiction & Fantasy",Price = 255,Stock = 4 },

                new Book { Name = "Witch's Bell", Author ="Odette C Bell",
                Description = "An urban fantasy with everything from romance to mystery, The Witch's Bell Series follow a feisty witch, Ebony Bell, as she solves magical malady after magical malady.",
                Genre ="Science Fiction & Fantasy",Price = 100,Stock = 5 },

                new Book { Name = "Yin and Yang: A Fool's Beginning", Author ="Odette C Bell",
                Description = "Yin is the Savior of the ages. A young woman chosen to protect the world on its final day. Captain Yang has never met a woman like her. Impetuous, powerful, and determined, she’s too much to handle.",
                Genre ="Science Fiction & Fantasy",Price = 180,Stock = 5 },

                //Arts & Entertainment (8 books)
                new Book { Name = "A Short History of Everything Neatly", Author ="Bill Bryson",
                Description = " Bill Bryson describes himself as a reluctant traveller, but even when he stays safely at home he can't contain his curiosity about the world around him. A Short History of Nearly Everything is his quest to understand everything that has happened from the Big Bang to the rise of civilization - how we got from there, being nothing at all, to here, being us.",
                Genre ="Arts & Entertainment",Price = 151,Stock = 3 },

                new Book { Name = "Thunderbolt Kid", Author ="Bill Bryson",
                Description = "Bill Bryson’s first travel book opened with the immortal line, ‘I come from Des Moines. Somebody had to.’ In this deeply funny and personal memoir, he travels back in time to explore the ordinary kid he once was, in the curious world of 1950s Middle America. It was a happy time, when almost everything was good for you, including DDT, cigarettes and nuclear fallout. This is a book about one boy’s growing up. But in Bryson’s hands, it becomes everyone’s story, one that will speak volumes – especially to anyone who has ever been young.",
                Genre ="Arts & Entertainment",Price = 138,Stock = 3 },

                new Book { Name = "The Lost Continent", Author ="Bill Bryson",
                Description = "And, as soon as Bill Bryson was old enough, he left. Des Moines couldn’t hold him, but it did lure him back. After ten years in England, he returned to the land of his youth, and drove almost 14,000 miles in search of a mythical small town called Amalgam, the kind of trim and sunny place where the films of his youth were set. Instead, his search led him to Anywhere, USA; a lookalike strip of gas stations, motels and hamburger outlets populated by lookalike people with a penchant for synthetic fibres. He discovered a continent that was doubly lost; lost to itself because blighted by greed, pollution, mobile homes and television; lost to him because he had become a stranger in his own land.",
                Genre ="Arts & Entertainment",Price = 130,Stock = 2 },

                new Book { Name = "Down Under", Author ="Bill Bryson",
                Description = "It is the driest, flattest, hottest, most desiccated, infertile and climatically aggressive of all the inhabited continents and still Australia teems with life – a large portion of it quite deadly. In fact, Australia has more things that can kill you in a very nasty way than anywhere else.  Ignoring such dangers – and yet curiously obsessed by them – Bill Bryson journeyed to Australia and promptly fell in love with the country. And who can blame him? The people are cheerful, extrovert, quick-witted and unfailingly obliging: their cities are safe and clean and nearly always built on water; the food is excellent; the beer is cold and the sun nearly always shines. Life doesn’t get much better than this...",
                Genre ="Arts & Entertainment",Price = 151,Stock = 5 },

                new Book { Name = "The Road to Little Dribbling", Author ="Bill Bryson",
                Description = "Twenty years ago, Bill Bryson went on a trip around Britain to celebrate the green and kindly island that had become his adopted country. The hilarious book that resulted, Notes from a Small Island, was taken to the nation’s heart and became the bestselling travel book ever, and was also voted in a BBC poll the book that best represents Britain.Now, to mark the twentieth anniversary of that modern classic, Bryson makes a brand-new journey round Britain to see what has changed.",
                Genre ="Arts & Entertainment",Price = 152,Stock = 5 },

                new Book { Name = "Made in America", Author ="Bill Bryson",
                Description = "In Made in America, Bryson tells the story of how American arose out of the English language, and along the way, de-mythologizes his native land - explaining how a dusty desert hamlet with neither woods nor holly became Hollywood, how the Wild West wasn’t won, why Americans say ‘lootenant’ and ‘Toosday’, how they were eating junk food long before the word itself was cooked up - as well as exposing the true origins of the words G-string, blockbuster, poker and snafu.",
                Genre ="Arts & Entertainment",Price = 138,Stock = 2 },

                new Book { Name = "At Home", Author ="Bill Bryson",
                Description = "In At Home, Bill Bryson applies the same irrepressible curiosity, irresistible wit, stylish prose and masterful storytelling that made A Short History of Nearly Everything one of the most lauded books of the last decade, and delivers one of the most entertaining and illuminating books ever written about the history of the way we live.",
                Genre ="Arts & Entertainment",Price = 151,Stock = 2 },

                new Book { Name = "Neither Here, Nor There", Author ="Bill Bryson",
                Description = "Bill Bryson’s first travel book, The Lost Continent, was unanimously acclaimed as one of the funniest books in years. In Neither Here nor There he brings his unique brand of humour to bear on Europe as he shoulders his backpack, keeps a tight hold on his wallet, and journeys from Hammerfest, the northernmost town on the continent, to Istanbul on the cusp of Asia. Fluent in, oh, at least one language, he retraces his travels as a student twenty years before.",
                Genre ="Arts & Entertainment",Price = 140,Stock = 3 },

                //Religion & Spirituality (12 books)
                new Book { Name = "Celebrating Marriage", Author ="T.D Jakes",
                Description = "The fifth volume of the SIX PILLARS FROM EPHESIANS series is an examination of the marriage covenant of men and women and the church with Jesus. Here, T.D. Jakes explores this most intimate of relationships with the Lord, offering answers to key issues and struggles",
                Genre ="Religion & Spirituality",Price = 85,Stock = 6 },

                new Book { Name = "Destiny", Author ="T.D Jakes",
                Description = "Discover the divine purpose of your dreams with this insightful guide from Bishop T. D. Jakes -- and learn how Biblical principles will propel your life to the next level.Have you ever sensed the pull of a divine guide that was leading you to the right place or person? Destiny, that inner compass, directs you to fulfillment of your highest purpose",
                Genre ="Religion & Spirituality",Price = 139,Stock = 4 },

                new Book { Name = "Dont Drop the Mic", Author ="T.D Jakes",
                Description = "In Don't Drop the Mic, Bishop Jakes speaks to readers about communication and how the ways we speak and interact with others can be part of our everyday ministries. He helps readers understand:Why the way we speak and the words we use matterHow speaking well, no matter your topic or audience, improves your chances of getting the result you want",
                Genre ="Religion & Spirituality",Price = 140,Stock = 3 },

                new Book { Name = "Experiencing Jesus", Author ="T.D Jakes",
                Description = "Book 2 of Six Pillars From Ephesians. This book covers the spiritual workmanship of the believer, helping Christians discover the rich themes in the book of Ephesians. The studies are practical, challenging, and revealing and will empower readers to live at a new level of spiritual maturity. Students of the Word will want the complete set of six volumes.",
                Genre ="Religion & Spirituality",Price = 75,Stock = 5 },

                new Book { Name = "Identity", Author ="T.D Jakes",
                Description = "You have been uniquely created by God… to fulfill your divine purpose! In a day where so many people are frustrated, looking in different places to discover their life purpose and true meaning, you have the answer.  Look no further than who you are!",
                Genre ="Religion & Spirituality",Price = 168,Stock = 4 },

                new Book { Name = "Instinct", Author ="T.D Jakes",
                Description = "If you have ever felt misaligned, this book is for you. If you have lost the rhythm, the passion, or the thrill of living in alignment, then keep reading. As He did with the very cells that comprise our bodies and the dry bones that were joined together for new life, God has given us deeper instincts to be attracted to those things that fit a higher and better purpose.Never settle for less than God's best for your life.",
                Genre ="Religion & Spirituality",Price = 112,Stock = 3 },

                new Book { Name = "Intimacy with God", Author ="T.D Jakes",
                Description = "Book 3 of Six Pillars From Ephesians. This book covers the spiritual worship, of the believer, helping Christians discover the rich themes in the book of Ephesians. The studies are practical, challenging, and revealing and will empower readers to live at a new level of spiritual maturity. Students of the Word will want the complete set of six volumes.",
                Genre ="Religion & Spirituality",Price = 76,Stock = 6 },

                new Book { Name = "Life Overflowing", Author ="T.D Jakes",
                Description = "T.D. Jakes takes readers through the book of Ephesians chapter by chapter, teaching what it means for the Christian to have a life overflowing and how to walk worthy of the calling [they] have received. Beginning with the incredible love God has for his children and the plans he has for believers beyond their wildest dreams, Bishop Jakes goes on to explore true intimacy with God, the marriage relationship, and spiritual warfare. Here is Bishop Jakes's best teaching on Ephesians, conveniently packaged in one volume and now available in trade paper.",
                Genre ="Religion & Spirituality",Price = 170,Stock = 4 },

                new Book { Name = "Overcoming the Enemy", Author ="T.D Jakes",
                Description = "Book 6 of Six Pillars From Ephesians. This book covers the spiritual warfare of the believer, helping Christians discover the rich themes in the book of Ephesians. The studies are practical, challenging, and revealing and will empower readers to live at a new level of spiritual maturity. Students of the Word will want the complete set of six volumes.",
                Genre ="Religion & Spirituality",Price = 86,Stock = 2 },

                new Book { Name = "Soar", Author ="T.D Jakes",
                Description = "Too often we remain in jobs that stifle our souls and leave us on the runway of opportunity with the engine of our deepest passion stalled, watching others make their personal vision a reality and build a legacy for their children. But it's never too late to get your dreams off the ground -- God sees great things in your future! If you long to amplify your unique abilities, if you strive to balance personal fulfillment and professional satisfaction, if you dream of fulfilling God's destiny for you, then you are ready to Soar!",
                Genre ="Religion & Spirituality",Price = 140,Stock = 3 },

                new Book { Name = "The Great Investment", Author ="T.D Jakes",
                Description = "Bishop T. D. Jakes, preacher, author, motivator, and entrepreneur, is one of the most respected and influential voices in the country today. Now, in The Great Investment, Bishop Jakes empowers readers by laying out the blueprint for balanced successful living. He explains how the triad of faith, family, and finance is the cornerstone of a life of moral success—success based on God’s plan.",
                Genre ="Religion & Spirituality",Price = 126,Stock = 4 },

                new Book { Name = "When Women Pray", Author ="T.D Jakes",
                Description = "In a time when women carry more influence than any other generation, the power of prayer has never been more important to remind us that we do not have to bear our crosses alone. We need prayer to stand guard over our hearts and minds and over the hearts and minds of our families.Women today are shattering glass ceilings and forging new paths in the world. What Happens When a Woman Prays is a clarion call for women to continue their progressive march of empowerment by dreaming like their daughters and praying like their grandmothers.Through exploring the lives of 10 prayer - filled women of the Bible, Bishop Jakes emphasizes the life-changing power that women have when they find their identity, their strength, their healing, and their voices in Christ.",
                Genre ="Religion & Spirituality",Price = 168,Stock = 3 },

                //Biographies & Memoirs (12 books)
                new Book { Name = "Agent Zigzag" , Author= "Ben Macintyre",
                Description = "One December night in 1942, a Nazi parachutist landed in a Cambridgeshire field. His mission: to sabotage the British war effort. His name was Eddie Chapman, but he would shortly become MI5's Agent Zigzag. Dashing and louche, courageous and unpredictable, the traitor was a patriot inside, and the villain a hero. The problem for Chapman, his many lovers and his spymasters was knowing who he was. Ben Macintyre weaves together diaries, letters, photographs, memories and top-secret MI5 files to create the exhilarating account of Britain's most sensational double agent.",
                Genre = "Biographies & Memoirs",Price = 103 , Stock = 3 },

                new Book { Name = "Autopsy" , Author= "Ryan Blumenthal",
                Description = "As a medical detective of the modern world, forensic pathologist Ryan Blumenthal’s chief goal is to bring perpetrators to justice.He has performed thousands of autopsies, which have helped bring numerous criminals to book.",
                Genre= " Biographies & Memoirs",Price = 189 , Stock=2 },

                new Book { Name = "Fortunes" , Author= "Ebbe Dommisse", 
                Description = "A handful of Afrikaners have risen to the very top of the business world in South Africa in the past three decades, some of them now dollar billionaires with vast global business interests",
                Genre= "Biographies & Memoirs",Price = 209 , Stock=4 },

                new Book { Name = "Greenlights" , Author= "Matthew McConaughey", 
                Description = "From the Academy Award®-winning actor, an unconventional memoir filled with raucous stories, outlaw wisdom, and lessons learned the hard way about living with greater satisfaction",
                Genre= "Biographies & Memoirs",Price = 418, Stock=3 },

                new Book { Name = "Gunship Ace" , Author= "AI J Venter",
                Description = "Spotlights the career of a fascinating modern warrior, while also shedding light on some of the conflicts that have raged throughout the world” (Tucson Citizen).",
                Genre= "Biographies & Memoirs", Price = 95 , Stock= 5 },

                new Book { Name = "I Let Him Go", Author ="Denise Fergus",
                Description = "On 12th February 1993, Denise Fergus' life changed forever. As she was running errands at New Strand Shopping Centre, she let go of her two-year-old son's hand for a few seconds to take out her purse. Denise never saw her son again.”,",
                Genre ="Biographies & Memoirs",Price = 101,Stock = 3 },

                new Book { Name = "Quit Like A Woman", Author ="Holly Whitaker",
                Description = "When Holly Whitaker started to look for a way to recover, the support systems she found for recovery where archaic and patriarchal. Urging drinkers towards a newfound humility is great if you're a man, but if you're a woman and not in a position to renounce privileges you never had, a whole other approach is needed",
                Genre ="Biographies & Memoirs",Price = 104,Stock = 4 },

                new Book { Name = "Teacher’s Pet", Author ="Hayley McGregor",
                Description = "This is the shocking true story of a schoolgirl groomed by her teacher, and her courageous journey to heal the wrongs of her past.”,",
                Genre ="Biographies & Memoirs",Price = 97,Stock = 3 },

                new Book { Name = "The Sober Diaries", Author ="Clare Pooley",
                Description = "BY THE AUTHOR OF NEW YORK TIMES BESTSELLER THE AUTHENTICITY PROJECT, THE BRAVE AND FUNNY MEMOIR THAT IS CHANGING LIVES.How one mother gave up drinking and started living. This is Bridget Jones Dries Out",
                Genre ="Biographies & Memoirs",Price = 218,Stock = 4 },

                new Book { Name = "The Wild Silence", Author ="Raynor Winn",
                Description = "In 2016, days before they were unjustly evicted from their home, Raynor Winn was told her husband Moth was dying.Instead of giving up they embarked on a life-changing journey: walking the 630 - mile South West Coast Path, living by their wits, determination and love of nature.But all journeys must end and when the couple return to civilisation they find that four walls feel like a prison, cutting them off from the sea and sky that sustained them -that had saved Moth's life",
                Genre ="Biographies & Memoirs",Price = 211,Stock = 3 },

                new Book { Name = "Uncaptured", Author ="Mosilo Mothepu",
                Description = "Facing criminal charges and bankruptcy, unemployed and deemed a political risk, Mothepu experienced first-hand the loneliness of whistleblowing. The effect on her mental and physical health was devastating. Now, in Uncaptured, she recounts this troubling yet seminal chapter in her life with honesty, humility and wry humour in the hope that others who find themselves in a similar situation will follow in her footsteps and speak truth to power ",
                Genre ="Biographies & Memoirs",Price = 260,Stock = 4 },

                new Book { Name = "Untamed", Author ="Glennon Doyle",
                Description = "For many years, Glennon Doyle denied her discontent. Then, while speaking at a conference, she looked at a woman across the room and fell instantly in love. Three words flooded her mind: There. She. Is. At first, Glennon assumed these words came to her from on high but soon she realised they had come to her from within. This was the voice she had buried beneath decades of numbing addictions and social conditioning. Glennon decided to let go of the world's expectations of her and reclaim her true untamed self.",
                Genre ="Biographies & Memoirs",Price = 213,Stock = 5 },

                //Computers & Technology (12 books)
                new Book { Name = "A Tour of C++", Author ="Bjarne Stroustrup",
                Description = "The C++11 standard allows programmers to express ideas more clearly, simply, and directly, and to write faster, more efficient code. Bjarne Stroustrup, the designer and original implementer of C++, thoroughly covers the details of this language and its use in his definitive reference, The C++ Programming Language, Fourth Edition.",
                Genre ="Computers & Technology",Price = 207 ,Stock = 6 },

                new Book { Name = "Algorithms: Algorithms 4", Author ="Robert Sedgewick",
                Description = "This fourth edition of Robert Sedgewick and Kevin Wayne’s Algorithms is the leading textbook on algorithms today and is widely used in colleges and universities worldwide.",
                Genre ="Computers & Technology",Price = 528,Stock = 3 },

                new Book { Name = "The Art of SQL", Author ="Stephane Faroult",
                Description = "For all the buzz about trendy IT techniques, data processing is still at the core of our systems, especially now that enterprises all over the world are confronted with exploding volumes of data.",
                Genre ="Computers & Technology",Price = 182,Stock = 5 },

                new Book { Name = "Artificial Intelligence", Author ="Mohit Thakkar",
                Description = "It often happens that when we try to study a subject for some examination or a job interview, we just don’t find the right content.The problem with the reference books is that they are too descriptive for last moment studies. Whereas the problem with local publications is that they are inaccurate as compared to the reference books.",
                Genre ="Computers & Technology",Price = 99,Stock = 7 },

                new Book { Name = "ASP.NET 4", Author ="Stephen Walther",
                Description = "The most comprehensive book on Microsoft’s new ASP.NET 4, ASP.NET 4 Unleashed covers all facets of ASP.NET development. Led by Microsoft ASP.NET program manager Stephen Walther, an expert author team thoroughly covers the entire platform.",
                Genre ="Computers & Technology",Price = 388,Stock = 8 },

                new Book { Name = "ASP.NET MVC Framework", Author ="Stephen Walther",
                Description = "In this book, world-renowned ASP.NET expert and member of the Microsoft ASP.NET team Stephen Walther shows experienced developers how to use Microsoft’s new ASP.NET MVC Framework to build web applications that are more powerful, flexible, testable, manageable, scalable, and extensible.",
                Genre ="Computers & Technology",Price = 328,Stock = 4 },

                new Book { Name = "Clean Code", Author ="Robert C. Martin",
                Description = "Even bad code can function. But if code isn’t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn’t have to be that way.",
                Genre ="Computers & Technology",Price = 291,Stock = 5 },

                new Book { Name = "Computers For Seniors ", Author ="Nancy C. Muir",
                Description = "A computer provides a great resource for learning new things and keeping in touch with family and friends, but it may seem intimidating at first. The bestselling Computers For Seniors For Dummies is here to help the 50+ set conquer and overcome any uncertainty with clear-cut, easy-to-understand guidance on how to confidently navigate your computer and the Windows 10 operating system.",
                Genre ="Computers & Technology",Price = 291,Stock = 8 },

                new Book { Name = "Hackers", Author ="Steven Levy",
                Description = "This 25th anniversary edition of Steven Levy's classic book traces the exploits of the computer revolution's original hackers -- those brilliant and eccentric nerds from the late 1950s through the early '80s who took risks, bent the rules, and pushed the world in a radical new direction.",
                Genre ="Computers & Technology",Price = 82,Stock = 7 },

                new Book { Name = "PCs For Dummies", Author ="Dan Gookin",
                Description = "Completely updated to cover the latest technology and software, the 13th edition of PCs For Dummies tackles using a computer in friendly, human terms.",
                Genre ="Computers & Technology",Price = 291,Stock = 2 },

                new Book { Name = "SQL in 10 Minutes", Author ="Ben Forta",
                Description = "Whether you're an application developer, database administrator, web application designer, mobile app developer, or Microsoft Office users, a good working knowledge of SQL is an important part of interacting with databases. ",
                Genre ="Computers & Technology",Price = 174,Stock = 9 },

                new Book { Name = "The Art of Computer Programming", Author ="Donald E. Knuth",
                Description = "The bible of all fundamental algorithms and the work that taught many of today's software developers most of what they know about computer programming.",
                Genre ="Computers & Technology",Price = 598,Stock = 7 },

                //Thriller & Horror (12 books)
                new Book { Name = "1st to Die", Author ="James Patterson ",
                Description = "As the only woman homicide inspector in San Francisco, Lindsay Boxer has to be tough. But nothing she has seen prepares her for the horror of the honeymoon murders, when a brutal maniac begins viciously slaughtering newly wed couples on their wedding nights. Lindsay is sickened by the deaths, but her determination to bring the murderer to justice is threatened by her own personal tragedy.",
                Genre ="Thriller & Horror",Price = 217,Stock = 5 },

                new Book { Name = "2nd Chance", Author ="James Patterson ",
                Description = "When a little girl is shot on the steps of a San Francisco church, Detective Lindsay Boxer reconvenes the Women's Murder Club.",
                Genre ="Thriller & Horror",Price = 217,Stock = 8 },

                new Book { Name = "3rd Degree", Author ="James Patterson ",
                Description = "Detective Lindsay Boxer is jogging along a beautiful San Francisco street as a ferocious blast rips through the neighbourhood. A townhouse owned by an internet magnate explodes into flames, three people die and a sinister note signed 'August Spies' is found at the scene. ",
                Genre ="Thriller & Horror",Price = 184,Stock = 2 },

                new Book { Name = "Cat and Mouse", Author ="James Patterson ",
                Description = "Just as Alex Cross is beginning to feel that life is good and he is finally coming out of the depression he's been in since the death of his wife, he is called to Union Station train terminal - a man is on the loose, firing at random into the swarming crowds of travellers.",
                Genre ="Thriller & Horror",Price = 184,Stock = 4 },

                new Book { Name = "Cross", Author ="James Patterson ",
                Description = "Alex Cross was a rising star in Washington, DC, Police Department when an unknown shooter killed his wife, Maria, in front of him. Years later, having left the FBI and returned to practising psychology in Washington, DC, Alex finally feels his life is in order...",
                Genre ="Thriller & Horror",Price = 184,Stock = 0 },

                new Book { Name = "Double Cross", Author ="James Patterson ",
                Description = "Just when Alex Cross's life is calming down, he's drawn back into the game to confront the Audience Killer - a terrifying genius who stages his killings as public spectacles in Washington, DC and broadcasts them live on the net.",
                Genre ="Thriller & Horror",Price = 184,Stock = 4 },

                new Book { Name = "Four Blind Mice", Author ="James Patterson ",
                Description = "Alex Cross is preparing to resign from the Washington Police Force. He's enjoying the feeling; not least because the Mastermind is now in prison. And Alex has met a woman, Jamilla Hughes, and he is talking about the future. ",
                Genre ="Thriller & Horror",Price = 167,Stock = 2 },

                new Book { Name = "London Bridges", Author ="James Patterson ",
                Description = "Alex Cross is on vacation when he gets the call. A town in Nevada has been annihilated and the Russian super-criminal known as the Wolf is claiming responsibility. ",
                Genre ="Thriller & Horror",Price = 184,Stock = 7 },

                new Book { Name = "Mary, Mary", Author ="James Patterson ",
                Description = "Somebody is intent on murdering Hollywood's A-list. A well-known actress has been shot outside her Beverly Hills home. Shortly afterwards, the Los Angeles Times receives an email describing the murder in vivid detail. ",
                Genre ="Thriller & Horror",Price = 184,Stock = 6 },

                new Book { Name = "Roses are Red", Author ="James Patterson ",
                Description = "A series of meticulously planned bank robberies ends in murder, and detective Alex Cross must pit his wits against the bizarre and sadistic mastermind behind the crimes.",
                Genre ="Thriller & Horror",Price = 184,Stock = 8 },

                new Book { Name = "The Big Bad Wolf", Author ="James Patterson ",
                Description = "Alex Cross's first case since joining the FBI has his new colleagues perplexed. Across the country, men and women are kidnapped in broad daylight and then disappear completely.",
                Genre ="Thriller & Horror",Price = 184,Stock = 0 },

                new Book { Name = "Violets are Blue", Author ="James Patterson ",
                Description = "The Mastermind is back and he's hot on detective Alex Cross's trail. His cold, taunting threats leave Alex angry and deeply concerned for his family's safety. Two joggers have been found dead in San Francisco - bitten and hung by their feet to drain the blood. ",
                Genre ="Thriller & Horror",Price = 184,Stock = 0 },

                //Health, Mind & Body (12 books)
                new Book { Name = "The Path Made Clear", Author ="Oprah Winfrey",
                Description ="Everyone has a purpose. And, according to Oprah Winfrey, 'Your real job in life is to figure out as soon as possible what that is, who you are meant to be, and begin to honour your calling in the best way possible.'",
                Genre ="Health, Mind & Body", Price = 224, Stock= 4},

                new Book { Name = "The Wisdom of Sundays", Author = "Oprah Winfrey",
                Description =  "The Wisdom of Sundays features insightful selections from the most meaningful conversations between Oprah Winfrey and some of today's most admired thought leaders",
                Genre = "Health, Mind & Body", Price = 224, Stock = 2},

                new Book { Name ="What Happened to You?", Author =" Oprah Winfrey",
                Description ="Oprah Winfrey and renowned brain development and trauma expert, Dr Bruce Perry, discuss the impact of trauma and adverse experiences and how healing must begin with a shift to asking 'What happened to you?' rather than 'What’s wrong with you?'",
                Genre = "Health, Mind & Body", Price = 463, Stock= 5},

                new Book { Name = "What I Know For Sure", Author = "Oprah Winfrey",
                Description =  "Candid, moving, exhilarating, uplifting, and frequently humorous, the words Oprah shares in What I Know For Sure shimmer with the sort of truth that readers will turn to again and again.",
                Genre = "Health, Mind & Body", Price = 182, Stock = 3},

                new Book { Name ="Discover Your Destiny", Author ="Robin Sharma",
                Description ="A potent pathway to self-awakening that will help you to live your greatest life and claim the happiness, prosperity and inner peace that you deserve. From the author of the international bestseller, The Monk Who Sold His Ferrari",
                Genre ="Health, Mind & Body" , Price = 119, Stock = 3},

                new Book { Name = "The Monk Who Sold his Ferrari", Author = "Robin Sharma",
                Description = "An internationally bestselling fable about a spiritual journey, littered with powerful life lessons that teach us how to abandon consumerism in order to embrace destiny, live life to the full and discover joy.",
                Genre ="Health, Mind & Body", Price = 67, Stock = 5 },

                new Book { Name = "Life Lessons", Author ="Robin Sharma",
                Description ="101 inspirational lessons on how to achieve true happiness, find fulfilment and live peacefully and meaningfully every day, from Robin Sharma, leading life coach and author of the multi-million-copy bestseller The Monk Who Sold His Ferrari.",
                Genre = "Health, Mind & Body", Price = 116, Stock = 2},

                new Book { Name = "The 5 AM Club", Author = "Robin Sharma",
                Description = "Legendary leadership and elite performance expert Robin Sharma introduced The 5am Club concept over twenty years ago, based on a revolutionary morning routine that has helped his clients maximize their productivity, activate their best health and bulletproof their serenity in this age of overwhelming complexity.",
                Genre = "Health, Mind & Body" , Price = 323 , Stock = 6},

                new Book { Name = "The Greatness Guide", Author ="Robin Sharma",
                Description ="Robin Sharma, one of the world's top success coaches and author of the international bestseller ‘The Monk Who Sold His Ferrari’, offers 10 high-impact lessons for success.‘The Greatness Guide’ is a strikingly powerful and enormously practical handbook that will inspire you to get to world class in both your personal and professional life.Written by Robin Sharma, one of the world's top success coaches and a man whose ideas have been embraced by celebrity CEO's, leading entrepreneurs, rock stars and royalty, as well as by many FORTUNE 500 companies, ‘The Greatness Guide’ contains a proven formula that will help you meet your highest potential and live an extraordinary life.",
                 Genre = "Health, Mind & Body", Price = 119, Stock = 5},
 
                new Book{Name = "The Leader Who Had No Title", Author = "Robin Sharma",
                Description = "For more than fifteen years, Robin Sharma has been quietly sharing with Fortune 500 companies and many of the super-rich a success formula that has made him one of the most sought-after leadership advisers in the world. Now, for the first time, Sharma makes his proprietary process available to you, so that you can get to your absolute best while helping your organization break through to a dramatically new level of winning in these wildly uncertain times.",
                Genre = "Health, Mind & Body", Price = 262 ,Stock = 4},

                new Book{Name = "Leadership Wisdom", Author = "Robin Sharma",
                Description ="An internationally bestselling fable about a spiritual journey, littered with powerful life lessons that teach us how to abandon consumerism in order to embrace destiny, live life to the full and discover joy",
                Genre = "Health, Mind & Body", Price = 67, Stock = 3},

                new Book { Name = "Atomic Habits", Author = "James Clear",
                Description ="Transform your life with tiny changes in behaviour, starting now.",
                Genre = "Health, Mind & Body", Price = 240, Stock = 4},

        };
            books.ForEach(b => context.Books.AddOrUpdate(c => c.Name, b));
            context.SaveChanges();

        }
    }
}
