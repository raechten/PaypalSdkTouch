using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;
using PaypalSdkTouch;

namespace PayPalMobileSample2
{
	public partial class PayPalMobileSample2ViewController :UIViewController
	{
		private const string _productionPayPalClientId = @"YOUR PRODUCTION CLIENT ID HERE";
		private const string _sandboxPayPalClientId = @"YOUR SANDBOX CLIENT ID HERE";

		private readonly string _environment = PayPalMobile.PayPalEnvironmentNoNetwork;

		private PayPalPaymentViewController _paymentViewController;
		private SamplePayPalPaymentDelegate _samplePayPalPaymentDelegate;

		private PayPalFuturePaymentViewController _fpViewController;
		private SamplePayPalFuturePaymentDelegate _samplePayPalFuturePaymentDelegate;

		private PayPalConfiguration _payPalConfig;
		public string Environment { get; set; }

		public bool AcceptCreditCards { get; set; }

		public PayPalPayment CompletedPayment { get; set; }

		UIPopoverController FlipsidePopoverController;

		static bool UserInterfaceIdiomIsPhone { get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; } }

		public PayPalMobileSample2ViewController (IntPtr handle) : base (handle)
		{
		}

		#region View lifecycle
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "PayPal iOS Library Demo";

			// Initialize PayPal
			PayPalMobile.WithClientIds(_productionPayPalClientId, _sandboxPayPalClientId);

			_payPalConfig = new PayPalConfiguration
									{ 
										AcceptCreditCards = AcceptCreditCards, 
										LanguageOrLocale = "en",
										MerchantName = @"Awesome Shirts, Inc.",
										MerchantPrivacyPolicyURL = new NSUrl(@"https://www.paypal.com/webapps/mpp/ua/privacy-full"),
										MerchantUserAgreementURL = new NSUrl(@"https://www.paypal.com/webapps/mpp/ua/useragreement-full")
									};

			successView.Hidden = true;

			Environment = _environment;
			Debug.WriteLine ("PayPal iOS SDK version: {0}", PayPalMobile.LibraryVersion);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (true);
			PayPalMobile.PreconnectWithEnvironment(Environment);
		}

		#endregion

		partial void actAuthorizeFuturePayment (NSObject sender)
		{
			_samplePayPalFuturePaymentDelegate = new SamplePayPalFuturePaymentDelegate(this);
			_fpViewController = new PayPalFuturePaymentViewController(_payPalConfig, _samplePayPalFuturePaymentDelegate);

			if (_fpViewController.Handle == IntPtr.Zero) {
				Debug.WriteLine ("Failed to create PayPalFuturePaymentViewController.");
				return;
			}

			PresentViewController (_fpViewController, true, null);
		}

		partial void actFuturePaymentPurchase(NSObject sender)
		{
			// Display activity indicator...
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			try
			{
				var correlationId = PayPalMobile.ApplicationCorrelationIDForEnvironment(Environment);
				var msg = string.Format("Correlation Id for this pre-authorized purchase: {0}\n\nSend correlationId and transaction details to your server for processing with PayPal.", correlationId);
				(new UIAlertView("Future Purchase", msg, null, "Ok")).Show();
			}
			finally
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			}
		}

		partial void actPay (NSObject sender)
		{
			// Remove our last completed payment, just for demo purposes.
			CompletedPayment = null;

			var payment = new PayPalPayment  
			{
				Amount = new NSDecimalNumber("9.95"),
				CurrencyCode = "USD",
				ShortDescription = "Hipster t-shirt",
				Intent = PayPalPaymentIntent.Sale
			};

			if (!payment.Processable) {
				// This particular payment will always be processable. If, for
				// example, the amount was negative or the shortDescription was
				// empty, this payment wouldn't be processable, and you'd want
				// to handle that here.
			}

			_payPalConfig.AcceptCreditCards = AcceptCreditCards;

			_samplePayPalPaymentDelegate = new SamplePayPalPaymentDelegate(this);
			_paymentViewController = new PayPalPaymentViewController(payment, _payPalConfig, _samplePayPalPaymentDelegate);

			if (_paymentViewController.Handle == IntPtr.Zero) {
				Debug.WriteLine ("Failed to create PayPalPaymentViewController.");
				return;
			}

			PresentViewController (_paymentViewController, true, null);
		}

		private void SendCompletedPaymentToServer (PayPalPayment completedPayment)
		{
			// TODO: Send completedPayment.confirmation to server
			Debug.WriteLine ("Here is your proof of payment:\n\n{0}\n\nSend this to your server for confirmation and fulfillment.", completedPayment.Confirmation);
		}

		// TODO: Expose this as a WeakDelegate through PayPalPaymentDelegate
		public void PayPalPaymentDidComplete (PayPalPayment completedPayment)
		{
			Debug.WriteLine ("PayPal Payment Success!");
			CompletedPayment = completedPayment;
			successView.Hidden = false;

			// Payment was processed successfully; send to server for verification and fulfillment
			SendCompletedPaymentToServer (completedPayment);
			DismissViewController (false, null);
		}

		// TODO: Expose this as a WeakDelegate through PayPalPaymentDelegate
		public void PayPalPaymentDidCancel ()
		{
			Debug.WriteLine ("PayPal Payment Canceled");
			CompletedPayment = null;
			successView.Hidden = true;
			DismissViewController (true, null);
		}

		public void PayPalPaymentDidCancelFuturePayment()
		{
			DismissViewController(true, null);
		}

		private void SendAuthorizationToServer(NSDictionary futurePaymentAuthorization)
		{
			//NSError error;
			//var consentJSONData = NSJsonSerialization.Serialize(futurePaymentAuthorization, NSJsonWritingOptions.PrettyPrinted, out error);
			// TODO: Send completedPayment.confirmation to server
			Debug.WriteLine("Here is your Future Payment Authorization:\n\n{0}\n\nSend this to your server for further processing.", futurePaymentAuthorization);
		}

		public void PayPalAuthorizedFuturePayment(NSDictionary futurePaymentAuthorization)
		{
			// The user has successfully logged into PayPal, and has consented to future payments.
			// Your code must now send the authorization response to your server.
			SendAuthorizationToServer(futurePaymentAuthorization);

			// Be sure to dismiss the PayPalLoginViewController.
			DismissViewController(true, null);
		}

		// Flipside View Controller
		public void FlipsideViewControllerDidFinish (FlipsideViewController flipsideViewController)
		{
			if (UserInterfaceIdiomIsPhone) 
			{
				DismissViewController (true, null);
			} 
			else 
			{
				FlipsidePopoverController.Dismiss (true);
				FlipsidePopoverController = null;
			}
		}

		[Export("popoverControllerDidDismissPopover:")]
		public void PopoverControllerDidDismiss (UIPopoverController popoverController)
		{
			FlipsidePopoverController = null;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if (segue.Identifier == "showAlternate") 
			{
				((FlipsideViewController)segue.DestinationViewController).Parent = this; 

				if (!UserInterfaceIdiomIsPhone) 
				{
					var popoverController = ((UIStoryboardPopoverSegue)segue).PopoverController;
					FlipsidePopoverController = popoverController;
					popoverController.WeakDelegate = this;
				}
			}
		}

		partial void actTogglePopover (NSObject sender)
		{
			if (FlipsidePopoverController != null) 
			{
				FlipsidePopoverController.Dismiss (true);
				FlipsidePopoverController = null;
			} 
			else 
			{
				PerformSegue ("showAlternate", sender);
			}
		}
	}
}

