namespace Common.DbPromotion
{
    public static class Seeding
    {
        public const string BlockSetIdSkyrim001 = "bs-skyrim-001";

        public static BuildingBlockSet CreateBlockSetForCampaign(string blockSetId)
        {
            switch (blockSetId)
            {
                case BlockSetIdSkyrim001:
                    return CreateBlockSetSkyrim_001();
                default:
                // Ouch
                break;
            }

            throw new KeyNotFoundException("Couldn'T find id of block set");
        }

        private static BuildingBlockSet CreateBlockSetSkyrim_001()
        {
            var myBlockSet = new BuildingBlockSet();
            myBlockSet.Id = BlockSetIdSkyrim001;
            myBlockSet.Name = "Skyrim Campaign 01";
           // myBlockSet.Version = Skyrim_001_Version.ToString();
            myBlockSet.OrderedListOfBlockSets = new List<BlockSet>();
            myBlockSet.CultureInfoOfBlockSet = "en";

            myBlockSet.OrderedListOfBlockSets.Add(new BlockSet
            {
                Description = "Salutation",
                SequenceNumber = 0,
                TextModules = BuildSalutations()
            });

            myBlockSet.OrderedListOfBlockSets.Add(new BlockSet
            {
                Description = "Introduction",
                SequenceNumber = 1,
                TextModules = BuildApology()
            });

            myBlockSet.OrderedListOfBlockSets.Add(new BlockSet
            {
                Description = "Smalltalk",
                SequenceNumber = 2,
                TextModules = BuildSmallTalkSkyrim()
            });

            myBlockSet.OrderedListOfBlockSets.Add(new BlockSet
            {
                Description = "Petition",
                SequenceNumber = 3,
                TextModules = BuildPetitionSkyrim()
            });
            myBlockSet.OrderedListOfBlockSets.Add(new BlockSet
            {
                Description = "Subject",
                SequenceNumber = 4,
                TextModules = new List<string> { "https://youtu.be/JnxLrfwc4y8" }
            });
            myBlockSet.OrderedListOfBlockSets.Add(new BlockSet
            {
                Description = "Greeting",
                SequenceNumber = 5,
                TextModules = BuildGreeting()
            });
            myBlockSet.OrderedListOfBlockSets.Add(new BlockSet
            {
                Description = "PS",
                SequenceNumber = 6,
                TextModules = BuildAddMeAsFriend()
            });

            return myBlockSet;
        }

        public static List<string> BuildPetitionSkyrim()
        {
            return new List<string>
                   {
                       "If you are also interested in mods, beautiful pictures and music, you can click and subscribe of course:-)",
                       "Maybe you'll like it too. I'm sure he'd be happy about a subscription :-)",
                       "If you like mods (and Skyrim music, of course), you should definitely click on it :-)",
                       "Maybe you would like to click on it. It's worth it. There are beautiful pictures and skyrim music :-)"
                   };
        }

        public static List<string> BuildSmallTalkSkyrim()
        {
            return new List<string>
                   {
                       "I'm a big Skyrim fan and saw you in a Skyrim group. One of my acquaintances made this video and this piece of music.",
                       "I'm a Skyrim nerd and saw you in a Skyrim group. One of my friends made this video and this piece of music.",
                       "I love Skyrim and saw you were in a group too. A friend of mine makes music and has put this video on Youtube",
                       "I am a little dragon blood and I saw that you are also in a group. A distant acquaintance of mine uploaded this video to Youtube.",
                       "I'm a big Skyrim fan and saw you in a Skyrim group. A distant acquaintance of mine uploaded this video to Youtube.",
                       "I love Skyrim and saw you were in a group too. One of my friends made this video and this piece of music."
                   };
        }

        public static List<string> BuildApology()
        {
            return new List<string>
                   {
                       "Please excuse the harassment.",
                       "Please excuse the annoyance.",
                       "Please excuse the disturbance.",
                       "I don't want to bother you for long.",
                       "I don't want to bother you long.",
                       "I don't want to disturb you for long.",
                       "I hope I'm not disturbing you too much.",
                       "Please excuse the intrusive message.",
                       "Please forgive the intrusive message."
                   };

        }

        public static List<string> BuildAddMeAsFriend()
        {
            return new List<string>
                   {
                       "Feel free to add :-)",
                       "Add me if you like:-)",
                       "You can add me, I am real:-)"
                   };
        }

        public static List<string> BuildGreeting()
        {
            return new List<string>
                   {
                       "Best regards",
                       "Best wishes",
                       "Salutations",
                       "Kind regards",
                       "Sincere regards",
                       "Sincerely",
                       "Warm regards"
                   };
        }

        public static List<string> BuildSalutations()
        {
            return new List<string>
                   {
                      "Hello there",
                      "Hello",
                      "Howdy",
                      "Hi",
                      "G'day",
                      "Heyday",
                      "Hey"
                   };
        }
    }
}
