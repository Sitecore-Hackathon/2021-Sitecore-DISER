namespace SitecoreDiser.Feature.ContentReport
{
    public class Constants
    {      
       
        public const string Database = "master";
        public const string Archive = "archive";
       
        public struct Fields
        {

            public const string Title = "Title";
            public const string FromDateLabel = "From Date Label";
            public const string ToDateLabel = "To Date Label";

        }
        public struct Indexes
        {
            public struct ContentIndex
            {
                public struct Fields
                {
                    public const string IsLatestVersion = "_latestversion";
                    public const string LastUpdatedDate = "_small_updated_date";
                    public const string BoostValue = "boost_value_tl";
                    public const string FullPath = "_fullpath";
                    public const string UpdatedDate = "__smallupdateddate_tdt";
                    public const string WorkflowState = "__workflow_state";
                    public const string WorkflowVersion = "_version";
                    public const string ComputedSourcePath = "c_sourcepath_sm";
                }
            }
        }
    }
}