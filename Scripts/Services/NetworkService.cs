using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Services
{
    public class NetworkService
    {
        private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
        {
            using UnityWebRequest request = (form == null) ?
                UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form);
		
            yield return request.SendWebRequest();

            switch (request.result) {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError($"network problem: {request.error}");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"response error: {request.responseCode}");
                    break;
                case UnityWebRequest.Result.InProgress:
                case UnityWebRequest.Result.Success:
                case UnityWebRequest.Result.DataProcessingError:
                default:
                    callback(request.downloadHandler.text);
                    break;
            }
        }
	
        public IEnumerator LogMessage(string url, string message, Action<string> callback) {
            WWWForm form = new WWWForm();
            form.AddField("message", message);
            form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

            return this.CallAPI(url, form, callback);
        }
    }
}
