﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GCR.Core;
using GCR.Web.Infrastructure;
using Newtonsoft.Json;


namespace GCR.Web.Models
{
    public class StatusMessage
    {
        public StatusMessage(MessageMode status, string message)
            : this(status, message, null, null)
        {
        }

        public StatusMessage(MessageMode status, string message, string returnUrl)
            : this(status, message, returnUrl, null)
        {
        }

        public StatusMessage(MessageMode status, string message, string returnUrl, object extraData)
        {
            this.Status = status;
            this.Message = message;
            this.ReturnUrl = returnUrl;
        }

        [JsonIgnore]
        public MessageMode Status { get; set; }

        public string status { get { return ResolveMessageMode(this.Status); } }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "returnUrl")]
        public string ReturnUrl { get; set; }

        [JsonIgnore]
        public object ExtraData { get; set; }

        public object extraData { get { return this.ExtraData; } }


        private string ResolveMessageMode(MessageMode mode)
        {
            switch (mode)
            {
                case MessageMode.Error:
                    return "error";
                case MessageMode.Notice:
                    return "notice";
                case MessageMode.Success:
                    return "success";
                case MessageMode.Info:
                    return "info";
                default:
                    return "error";
            }
        }
    }

    public class HttpErrorViewModel : HandleErrorInfo
    {
        public HttpErrorViewModel(Exception exception, string controllerName, string actionName)
            : base(exception, controllerName, actionName)
        {
            var http = exception as HttpException;
            if (http != null)
            {
                this.StatusCode = http.GetHttpCode();
            }
            else
            {
                this.StatusCode = 500;
            }
            this.StatusDescription = HttpStatusDescription.Get(this.StatusCode);
        }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string RequestedUrl { get; set; }
        public string ReferrerUrl { get; set; }
    }
}
