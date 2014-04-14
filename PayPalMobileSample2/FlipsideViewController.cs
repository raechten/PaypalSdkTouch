using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;
using PaypalSdkTouch;

namespace PayPalMobileSample2
{
	public partial class FlipsideViewController : UIViewController
	{
		public FlipsideViewController (IntPtr handle) : base (handle)
		{
		}

		public PayPalMobileSample2ViewController Parent { get; set; }

		private void LogEnvironment ()
		{
			Debug.WriteLine ("Environment: {0}. Accept credit cards? {1}", Parent.Environment, Parent.AcceptCreditCards);
		}	

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			LogEnvironment ();

			if (PayPalMobile.PayPalEnvironmentProduction == Parent.Environment) {
				environmentSegmentedControl.SelectedSegment = 2;
			} else if (PayPalMobile.PayPalEnvironmentSandbox == Parent.Environment) {
				environmentSegmentedControl.SelectedSegment = 1;
			} else if (PayPalMobile.PayPalEnvironmentNoNetwork == Parent.Environment) {
				environmentSegmentedControl.SelectedSegment = 0;
			}

			acceptCreditCards.On = Parent.AcceptCreditCards;

			if (Parent.CompletedPayment != null) {
				Debug.WriteLine (Parent.CompletedPayment.ToString());
				proofOfPaymentTextView.Text = Parent.CompletedPayment.ToString();
			} else {
				proofOfPaymentTextView.Hidden = true;
				proofOfPaymentLabel.Hidden = true;
			}
			proofOfPaymentTextView.Layer.CornerRadius = 8.0f;
		}

		partial void environmentControlDidUpdate (NSObject sender)
		{
			switch (environmentSegmentedControl.SelectedSegment) {
				case 0:
					Parent.Environment = PayPalMobile.PayPalEnvironmentNoNetwork;
					break;
				case 1:
					Parent.Environment = PayPalMobile.PayPalEnvironmentSandbox;
					break;
				default:
					Parent.Environment = PayPalMobile.PayPalEnvironmentProduction;
					break;
			}

			LogEnvironment ();
		}

		partial void processCreditCardsChanged (NSObject sender)
		{
			Parent.AcceptCreditCards = acceptCreditCards.On;
			LogEnvironment ();
		}

		partial void actDone (NSObject sender)
		{
			LogEnvironment ();
			Parent.FlipsideViewControllerDidFinish(this);
		}
	}
}
