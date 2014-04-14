using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;
using PaypalSdkTouch;

namespace PayPalMobileSample2
{
	public class SamplePayPalPaymentDelegate : PayPalPaymentDelegate
	{
		private readonly PayPalMobileSample2ViewController _hostViewController;

		public SamplePayPalPaymentDelegate(PayPalMobileSample2ViewController hostViewController)
		{
			_hostViewController = hostViewController;
		}

		public override void DidCancelPayment(PayPalPaymentViewController paymentViewController)
		{
			_hostViewController.PayPalPaymentDidCancel();
		}

		public override void DidCompletePayment(PayPalPaymentViewController paymentViewController, PayPalPayment completedPayment)
		{
			_hostViewController.PayPalPaymentDidComplete(completedPayment);
		}
	}
	
}
