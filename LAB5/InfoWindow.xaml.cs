using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace LAB5
{
    /// <summary>
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        private string _accessToken;
        private VkApi _vkApi;
        private string reqStrTemplate = "https://api.vk.com/method/{0}?access_token={1}&v=5.199";

        public class UserResponse
        {
            [JsonProperty("response")]
            public User _user { get; set; }
        }

        public class UserInfoResponse
        {
            [JsonProperty("response")]
            public UserRegion _userRegion { get; set; }
        }

        public class GroupsWithoutProfilesResponse
        {
            [JsonProperty("response")]
            public List<Group> Response { get; set; }
        }

        public class GroupsResponse
        {
            [JsonProperty("response")]
            public GroupsWithoutProfilesResponse response { get; set; }
        }

        public class User
        {
            [JsonProperty("last_name")]
            public string lastName { get; set; }
            [JsonProperty("first_name")]
            public string firstName { get; set; }
            [JsonProperty("bdate")]
            public string birthDate { get; set; }
        }

        public class UserRegion
        {
            [JsonProperty("country")]
            public string country { get; set; }
        }

        public class Group
        {
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("group_id")]
            public string groupId { get; set; }
            [JsonProperty("members_count")]
            public string membersAmount { get; set; }
        }

        public InfoWindow(string acessToken, string userID)
        {
            InitializeComponent();
            _accessToken = acessToken;
            _vkApi = new VkApi();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string method1 = "account.getProfileInfo";
            string method2 = "account.getInfo";
            string f1 = _vkApi.Get(reqStrTemplate, method1, _accessToken);
            string f2 = _vkApi.Get(reqStrTemplate, method2, _accessToken);
            var user = JsonConvert.DeserializeObject<UserResponse>(f1);
            var userInfo = JsonConvert.DeserializeObject<UserInfoResponse>(f2);

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(user._user.lastName + " " + user._user.firstName + 
                "\nДата рождения: " + user._user.birthDate +
                "\nРегион пользователя: " + userInfo._userRegion.country);
            textBox1.Clear();
            textBox1.Text = strBuilder.ToString();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string method = "groups.get";
            int count = 10;
            int offset = 0;
            string f = _vkApi.Get(reqStrTemplate, method, _accessToken, $"count={count}&offset={offset}");

            var groupsIds = JsonConvert.DeserializeObject(f) as JObject;
            JArray groupsIdsArr = (JArray)groupsIds["response"]["items"];

            method = "groups.getById";
            StringBuilder strBuilder = new StringBuilder();

            string idsArr = string.Join(",", groupsIdsArr);
            string fields = "members_count";
            string groupsInfo = _vkApi.Get(reqStrTemplate, method, _accessToken, $"group_ids={idsArr}&fields={fields}");
            var groupsData = JsonConvert.DeserializeObject<dynamic>(groupsInfo);

            if (groupsData.response != null && groupsData.response.groups != null)
            {
                foreach (var group in groupsData.response.groups)
                {
                    string groupId = group.id;
                    string groupName = group.name;
                    int membersCount = group.members_count;
                    strBuilder.AppendLine($"ID группы: {groupId}, название группы: {groupName}, количество подпищиков: {membersCount}");
                }
            }

            textBox1.Clear();
            textBox1.Text = strBuilder.ToString();
        }
    }
}
