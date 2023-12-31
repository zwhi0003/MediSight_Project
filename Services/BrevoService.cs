﻿using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace MediSight_Project.Services
{
    public class BrevoService
    {
        public async Task<string> SendEmail(
        string senderName,
        string senderEmail,
        string recipientName,
        string recipientEmail,
        string subject,
        string htmlContent)
        {
            
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.brevo.com/v3/smtp/email");
                // get the client
                var request = new HttpRequestMessage(HttpMethod.Post, "");
                // add request headers
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // add the api key to the headers
                request.Headers.Add("api-key", "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");
                // build the data
                string jsonData = $@"{{
                    ""sender"": {{
                        ""name"": ""{senderName}"",
                        ""email"": ""{senderEmail}""
                    }},
                    ""to"": [
                        {{
                            ""email"": ""{recipientEmail}"",
                            ""name"": ""{recipientName}""
                        }}
                    ],
                    ""subject"": ""{subject}"",
                    ""htmlContent"": ""{htmlContent}""
                }}";

                // add the content
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
                // make the request and handle the errors
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    return "Error: " + response.StatusCode.ToString();
                }
            }
            
            
        }
    }

}