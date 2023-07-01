using Newtonsoft.Json;
using SovosWebApi.Core.Models;
using System.Net.Http.Json;
using System.Text;

namespace SovosWebApi.UnitTest
{
    public class InvoiceHeaderTest
    {
        private readonly string basePath = "http://localhost:5000/";
        private HttpClient client;
        public InvoiceHeaderTest()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(60);
        }

        [Fact]
        public void CreateInvoiceHeader()
        {
            string requestUri = basePath + "InvoiceHeader/Create";

            InvoiceHeader invoiceHeader = new InvoiceHeader()
            {
                InvoiceId = Guid.NewGuid().ToString().Substring(0, 15),
                Date = DateTime.Now.Date,
                ReceiverTitle = "test",
                SenderTitle = "test",
            };
            invoiceHeader.Invoices.Add(new InvoiceLine()
            {
                Name = "test",
                Quantity = 1,
                UnitCode = "TST",
                UnitPrice = 55,
                InvoiceId = invoiceHeader.InvoiceId
            });

            string jsondata = JsonConvert.SerializeObject(invoiceHeader);
            HttpContent content = new StringContent(jsondata);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var res = client.PostAsync(requestUri, content).GetAwaiter().GetResult();

            Assert.False(res.StatusCode != System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void GetAllInvoiceHeader()
        {
            string requestUri = basePath + "InvoiceHeader/GetAll";
            var res = client.GetAsync(requestUri).GetAwaiter().GetResult();
            var body = res.Content.ReadAsStringAsync().Result;

            Assert.False(res.StatusCode != System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void GetInvoiceAllDetail()
        {
            string requestUri = basePath + "InvoiceHeader/GetInvoiceAllDetail";
            string id = "be0df57a-5225-4";
            var res = client.GetAsync(requestUri + "/" + id).GetAwaiter().GetResult();
            var data = res.Content.ReadAsByteArrayAsync().Result;

            var invoiceDetailList = JsonConvert.DeserializeObject<List<InvoiceLine>>(Encoding.UTF8.GetString(data), new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            Assert.False(res.StatusCode != System.Net.HttpStatusCode.OK);
        }
    }
}