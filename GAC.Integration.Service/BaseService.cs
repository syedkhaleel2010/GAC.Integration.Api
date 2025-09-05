﻿using GAC.Integration.Service.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace GAC.Integration.Service
{
    public class BaseService
    {
        private readonly IUserSession _userSession;

        public BaseService(IUserSession userSession)
        {
            _userSession = userSession;
        }

        protected string GetUsername()
        {
            return _userSession?.GetUser()?.Name;
        }

        protected void SetCreatedBy(EntityBase entity)
        {
            entity.CreatedBy = GetUsername() ?? "system";
            entity.CreatedDate = DateTime.Now;
        }

        protected void SetUpdatedBy(EntityBase entity)
        {
            entity.UpdatedBy = GetUsername() ?? "system";
            entity.UpdatedDate = DateTime.Now;
        }

        protected async Task<HttpClient> AddTokenWithHttpClient(HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
            return httpClient;
        }
        protected async Task<T> ValidataServiceResponseWithContent<T>(HttpResponseMessage response, string dataToken)
        {
            var finalResponse = await ValidateResponseSuccess<JObject>(response, "", dataToken);

            return finalResponse == null ? default(T) : finalResponse.ToObject<T>();
        }

        /// <summary>
        /// Validates the response and Converts Data.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="response"></param>
        /// <param name="apiName"> Use this param Only if response is formatted in apiNameResponse.apiNameResult Style</param>
        /// <param name="dataTokenName">Use This param when response is formatted in custom data token</param>
        /// <returns></returns>
        protected async Task<TData> ValidateResponseSuccess<TData>(HttpResponseMessage response, string apiName = "", string dataTokenName = "") where TData : JToken
        {
            if (!response.IsSuccessStatusCode)
            {
                var exPlainResponse = await response.Content.ReadAsStringAsync();
                string exMessage = string.Empty;
                if (!string.IsNullOrWhiteSpace(exPlainResponse))
                {
                    var exResult = JObject.Parse(exPlainResponse);
                    exMessage = (exResult.SelectToken("msg") ?? JValue.CreateNull()).ToObject<string>();
                    exMessage = exMessage?.Replace("\n", " ").Replace("\r", " ");

                    var responseMsg = new HttpResponseMessage(response.StatusCode)
                    {
                        Content = new StringContent(exMessage ?? JsonConvert.SerializeObject(exResult)),
                        ReasonPhrase = exMessage ?? JsonConvert.SerializeObject(exResult)
                    };

                    //throw new HttpResponseException(responseMsg);

                }

                throw new HttpRequestException($"Response returned error code {response.StatusCode}.{Environment.NewLine}Response content:{exMessage}");
            }

            JToken dataToken;
            string tokenName = string.Empty;
            var plainResponse = await response.Content.ReadAsStringAsync();
            var jResult = JObject.Parse(plainResponse);

            if (String.IsNullOrEmpty(apiName))
            {
                tokenName = String.IsNullOrEmpty(dataTokenName) ? "" : dataTokenName;
            }
            dataToken = jResult.SelectToken(tokenName);
            if (dataToken != null && dataToken.HasValues)
            {
                var jData = (TData)dataToken;
                return jData;
            }

            return null;
        }

    }
}

