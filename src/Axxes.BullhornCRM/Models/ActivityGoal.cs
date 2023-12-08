using Axxes.BullhornCRM.Attributes;
using Newtonsoft.Json;

namespace Axxes.BullhornCRM.Models;

[EntityName(nameof(ActivityGoal))]
public class ActivityGoal : IBullhornEntity
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("user")] public UserClass User { get; set; }

    [JsonProperty("department")] public DepartmentClass Department { get; set; }

    [JsonProperty("activityType")] public string ActivityType { get; set; }

    [JsonProperty("startDate")] public DateTimeOffset StartDate { get; set; }

    [JsonProperty("endDate")] public DateTimeOffset EndDate { get; set; }

    [JsonProperty("goal")] public long Goal { get; set; }

    [JsonProperty("actual")] public long Actual { get; set; }

    [JsonProperty("periodName")] public PeriodNameEnum PeriodName { get; set; }

    [JsonProperty("percentAttained")] public long PercentAttained { get; set; }

    public class DepartmentClass
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public Name Name { get; set; }
    }

    public class UserClass
    {
        [JsonProperty("firstName")] public string FirstName { get; set; }

        [JsonProperty("lastName")] public string LastName { get; set; }
    }

    public enum Name
    {
        Hr,
        Sales
    };

    public enum PeriodNameEnum
    {
        Monthly,
        Weekly
    }
}
