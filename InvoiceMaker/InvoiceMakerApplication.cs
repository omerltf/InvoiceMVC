using InvoiceMaker.Data;
using InvoiceMaker.Initialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace InvoiceMaker
{
    public class InvoiceMakerApplication : HttpApplication
    {
        protected void HandleBeginRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleBeginRequest");
        }

        public override void Init()
        {
            base.Init(); // Included to let the base class
                         // run any initialization code.

            BeginRequest += HandleBeginRequest;
            AuthenticateRequest += HandleAuthenticateRequest;
            PostAuthenticateRequest += HandlePostAuthenticateRequest;
            AuthorizeRequest += HandleAuthorizeRequest;
            PostAuthorizeRequest += HandlePostAuthorizeRequest;
            ResolveRequestCache += HandleResolveRequestCache;
            PostResolveRequestCache += HandlePostResolveRequestCache;
            MapRequestHandler += HandleMapRequestHandler;
            PostMapRequestHandler += HandlePostMapRequestHandler;

            // Uncomment these and subscribe to them like above
            AcquireRequestState += HandleAcquireRequestState;
            PostAcquireRequestState += HandlePostAcquireRequestState;
            PreRequestHandlerExecute += HandlePreRequestHandlerExecute;
             PostRequestHandlerExecute += HandlePostRequestHandlerExecute;
            ReleaseRequestState += HandleReleaseRequestState;
            PostReleaseRequestState += HandlePostReleaseRequestState;
            UpdateRequestCache += HandleUpdateRequestCache;
            PostUpdateRequestCache += HandlePostUpdateRequestCache;
             LogRequest += HandleLogRequest;
            PostLogRequest += HandlePostLogRequest;
            EndRequest += HandleEndRequest;
        }

        protected void Application_Start (object sender, EventArgs e)
        {
            Debug.WriteLine("Application Started");
            RouteConfiguration.AddRoutes(RouteTable.Routes);

            Database.SetInitializer(new DatabaseInitializer());
        }

        protected void Application_End (object sender, EventArgs e)
        {

        }

        private void HandleEndRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleEndRequest");
        }

        private void HandlePostLogRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostLogRequest");
        }

        private void HandleLogRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleLogRequest");
        }

        private void HandlePostUpdateRequestCache(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostUpdateRequestCache");
        }

        private void HandleUpdateRequestCache(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleUpdateRequestCache");
        }

        private void HandlePostReleaseRequestState(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostReleaseRequestState");
        }

        private void HandleReleaseRequestState(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleReleaseRequestState");
        }

        private void HandlePostRequestHandlerExecute(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostRequestHandlerExecute");
        }

        private void HandlePostAcquireRequestState(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostAcquireRequestState");
        }

        private void HandleAcquireRequestState(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleAcquireRequestState");
        }

        private void HandlePostMapRequestHandler(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostMapRequestHandler");
        }

        private void HandlePreRequestHandlerExecute(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostMapRequestHandler");
        }

        private void HandleMapRequestHandler(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleMapRequestHandler");
        }

        private void HandlePostResolveRequestCache(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostResolveRequestCache");
        }

        private void HandleResolveRequestCache(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleResolveRequestCache");
        }

        private void HandlePostAuthorizeRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostAuthorizeRequest");
        }

        private void HandleAuthorizeRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleAuthorizeRequest");
        }

        private void HandlePostAuthenticateRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandlePostAuthenticateRequest");
        }

        private void HandleAuthenticateRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("HandleAuthenticateRequest");
        }
    }
}