namespace Common.DbPromotion
{
    public class BuildingBlockSet
    {
        /// <summary>
        /// ID of the building block set.
        /// </summary>
        public string? Id { get; set; }

        ///// <summary>
        ///// Semantic version of the Building Block Set.
        ///// Major, e.g. a reduction in OrderedListOfBlockSets. 
        ///// Minor, e.g. an extension in one of the TextModules
        ///// Patch, e.g. a correction of a spelling mistake
        ///// </summary>
        //public string? Version { get; set; }
        
        /// <summary>
        /// Name of the building block set.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Each blockset contains text modules for building a full message.
        /// There might be text modules for greetings, salutations, etc.
        /// </summary>
        public List<BlockSet>? OrderedListOfBlockSets { get; set; }

        /// <summary>
        /// Determines the language of the current block set.
        /// </summary>
        public string? CultureInfoOfBlockSet { get; set; }
    }

    public class BlockSet
    {
        /// <summary>
        /// Description of blockset.
        /// I.e. salutation, small talk, hyperlink, greeting
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Number in the sequence.
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// Available text modules.
        /// I.e. different salutations, greetings, etc.
        /// </summary>
        public List<string>? TextModules { get; set; }
    }
}
