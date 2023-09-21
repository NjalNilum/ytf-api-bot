using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Common.DbPromotion
{
    public class PromotionDb
    {
        private readonly MongoClient mongoClient;
        private readonly string internalDataBaseName;
        
        private IMongoCollection<BuildingBlockSet> collectionBlockSets;
        private IMongoCollection<AdvertisingCampaign> collectionAdvertisingCampaigns;


        private readonly string skyrimCampaign01 = "skyrim-campaign-01";

        public PromotionDb(string blockSetId, string campaignName)
        {
            // Establish connection and what not to database
            mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            internalDataBaseName = "promotion-campaigns";
            var internalDataBase = mongoClient.GetDatabase(internalDataBaseName);
            collectionBlockSets = internalDataBase.GetCollection<BuildingBlockSet>("building-block-sets");
            collectionAdvertisingCampaigns = internalDataBase.GetCollection<AdvertisingCampaign>("advertising-campaigns");

            // Update corresponding blockset
            var theFilter = Builders<BuildingBlockSet>.Filter.Eq(nameof(BuildingBlockSet.Id), blockSetId);
            collectionBlockSets.ReplaceOne(theFilter, Seeding.CreateBlockSetForCampaign(blockSetId));


            // do what
            CreateAdvertisingCampaign(this.skyrimCampaign01, internalDataBase, );




            

          
        }


        public void CreateAdvertisingCampaign(string nameOfCampaign, IMongoDatabase internalDataBase, BuildingBlockSet skyrimBlockset)
        {
            var campaignCollection = internalDataBase.GetCollection<BsonDocument>(nameOfCampaign);
            campaignCollection.InsertOne(BsonDocument.Create(skyrimBlockset));
            
            // Add blockset
            this.collectionBlockSets.InsertOne(skyrimBlockset);
            
            // Create and add advertisement
            var myCampaign = new AdvertisingCampaign();
            myCampaign.Id = Guid.NewGuid().ToString();
            myCampaign.Name = nameOfCampaign;
            myCampaign.Description = "Campaign to promote a video.";
            myCampaign.BuildingBlockSetId = skyrimBlockset.Id;

            this.collectionAdvertisingCampaigns.InsertOne(myCampaign);


        }
    }
}
