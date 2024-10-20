using System.Net.Http;

namespace ActivitySeeker.Desktop
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;
        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Активности";
            tabPage2.Text = "Типы Активностей";
            var result = await GetAllActivities();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async Task<string> GetAllActivities()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "Http://localhost:5199/activities");

            using var responsetext = await _httpClient.SendAsync(request);

            return await responsetext.Content.ReadAsStringAsync();
        }
    }
}
