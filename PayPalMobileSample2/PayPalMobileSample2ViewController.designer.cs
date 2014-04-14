// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace PayPalMobileSample2
{
	[Register ("PayPalMobileSample2ViewController")]
	partial class PayPalMobileSample2ViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton authorizeFuturePaymentsButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton futurePurchaseButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton payButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView successView { get; set; }

		[Action ("actAuthorizeFuturePayment:")]
		partial void actAuthorizeFuturePayment (MonoTouch.Foundation.NSObject sender);

		[Action ("actFuturePaymentPurchase:")]
		partial void actFuturePaymentPurchase (MonoTouch.Foundation.NSObject sender);

		[Action ("actPay:")]
		partial void actPay (MonoTouch.Foundation.NSObject sender);

		[Action ("actTogglePopover:")]
		partial void actTogglePopover (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (authorizeFuturePaymentsButton != null) {
				authorizeFuturePaymentsButton.Dispose ();
				authorizeFuturePaymentsButton = null;
			}

			if (payButton != null) {
				payButton.Dispose ();
				payButton = null;
			}

			if (successView != null) {
				successView.Dispose ();
				successView = null;
			}

			if (futurePurchaseButton != null) {
				futurePurchaseButton.Dispose ();
				futurePurchaseButton = null;
			}
		}
	}
}
