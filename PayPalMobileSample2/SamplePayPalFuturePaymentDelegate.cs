using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;
using PaypalSdkTouch;

namespace PayPalMobileSample2
{

	public class SamplePayPalFuturePaymentDelegate : PayPalFuturePaymentDelegate
	{
		private readonly PayPalMobileSample2ViewController _hostViewController;

		public SamplePayPalFuturePaymentDelegate(PayPalMobileSample2ViewController hostViewController)
		{
			_hostViewController = hostViewController;
		}

		public override void DidCancelFuturePayment(PayPalFuturePaymentViewController futurePaymentViewController)
		{
			_hostViewController.PayPalPaymentDidCancelFuturePayment();
		}

		public override void DidAuthorizeFuturePayment(PayPalFuturePaymentViewController futurePaymentViewController, NSDictionary futurePaymentAuthorization)
		{
			_hostViewController.PayPalAuthorizedFuturePayment(futurePaymentAuthorization);
		}
	}
	
}
