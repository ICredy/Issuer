﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FaberController.Enums;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FaberController.Services
{
    public class FCAgentService
    {
        public FCAgentService(HttpClient Http)
        {
            _http = Http;
        }

        private HttpClient _http { get; }

        public async Task<AgentStatus> GetStatus()
        {
            try
            {
                var response = await _http.GetAsync("/status");
                response.EnsureSuccessStatusCode();
                return AgentStatus.Up;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return AgentStatus.Down;
            }
        }

        public async Task<JArray> GetConnections()
        {
            try
            {
                var response = await _http.GetAsync("/connections");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(responseString);
                return jsonResponse.Value<JArray>("results");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JArray();
            }
        }

        public async Task<JObject> RemoveConnection(string connectionId)
        {
            if (connectionId == null || connectionId == "")
            {
                Console.Error.WriteLine("Must provide a connection ID");
                return new JObject();
            }
            try
            {
                using var content = new StringContent("");
                var response = await _http.PostAsync(string.Format("/connections/{0}/remove", connectionId), content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseString);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JObject();
            }
        }

        public async Task<JObject> CreateInvitation()
        {
            try
            {
                using var content = new StringContent("");
                var response = await _http.PostAsync("/connections/create-invitation", content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseString);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JObject();
            }
        }

        public async Task<JObject> ReceiveInvitation(string invitation)
        {
            try
            {

                using var content = new StringContent(invitation);
                var response = await _http.PostAsync("/connections/receive-invitation", content);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseString);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JObject();
            }
        }

        public async Task<JArray> GetSchemas()
        {
            try
            {
                var response = await _http.GetAsync("/schemas/created");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(responseString);
                return jsonResponse.Value<JArray>("schema_ids");

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JArray();
            }
        }


        public async Task<JObject> SetCustomSchema()
{
    try
    {

        Dictionary<string, object> schema_body = new Dictionary<string, object>(); 
        schema_body.Add("schema_name", "icredy");
        schema_body.Add("schema_version", "1.4");
        schema_body.Add("attributes", "marks");

        string serializedBody = JsonConvert.SerializeObject(schema_body);
        HttpContent query = new StringContent(serializedBody, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _http.PostAsync("/schemas", query);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        return JObject.Parse(responseString);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex);
        return new JObject();
    }
}


public async Task<JObject> SetCredDef()
{
    try
    {

        Dictionary<string, object> credential_definition_body = new Dictionary<string, object>(); 
        credential_definition_body.Add("schema_id", "JosAAp8vNBxgCuKCAmvcRr:2:icredy:1.4");
        credential_definition_body.Add("support_revocation", true);
        credential_definition_body.Add("revocation_registry_size", 1000);
        credential_definition_body.Add("tag", "Icredynew");

        string serializedBody = JsonConvert.SerializeObject(credential_definition_body);
        HttpContent query = new StringContent(serializedBody, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _http.PostAsync("/credential-definitions", query);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        return JObject.Parse(responseString);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex);
        return new JObject();
    }
}
        public async Task<JObject> GetSchema(string schemaId)
        {
            try
            {
                var response = await _http.GetAsync(string.Format("/schemas/{0}", schemaId));
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseString);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JObject();
            }
        }

        public async Task<JArray> GetCredentialDefinitions()
        {
            try
            {
                var response = await _http.GetAsync("/credential-definitions/created");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(responseString);
                return jsonResponse.Value<JArray>("credential_definition_ids");

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JArray();
            }
        }

        public async Task<JObject> GetCredentialDefinition(string credentialDefinitionId)
        {
            try
            {
                var response = await _http.GetAsync(string.Format("/credential-definitions/{0}", credentialDefinitionId));
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(responseString);
                return jsonResponse.Value<JObject>("credential_definition");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JObject();
            }
        }

        public async Task<JObject> SendCredential(string credential)
        {
            try
            {
                using var content = new StringContent(credential, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _http.PostAsync("/issue-credential/send", content);
                 response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                var responseString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(responseString);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new JObject();
            }
        }
    }
}