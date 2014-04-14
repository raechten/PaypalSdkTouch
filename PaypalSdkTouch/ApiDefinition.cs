using System;

using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;

namespace PaypalSdkTouch
{
	[BaseType (typeof (NSObject))]
	public partial interface PayPalConfiguration {

		[Export ("defaultUserEmail", ArgumentSemantic.Copy)]
		string DefaultUserEmail { get; set; }

		[Export ("defaultUserPhoneCountryCode", ArgumentSemantic.Copy)]
		string DefaultUserPhoneCountryCode { get; set; }

		[Export ("defaultUserPhoneNumber", ArgumentSemantic.Copy)]
		string DefaultUserPhoneNumber { get; set; }

		[Export ("merchantName", ArgumentSemantic.Copy)]
		string MerchantName { get; set; }

		[Export ("merchantPrivacyPolicyURL", ArgumentSemantic.Copy)]
		NSUrl MerchantPrivacyPolicyURL { get; set; }

		[Export ("merchantUserAgreementURL", ArgumentSemantic.Copy)]
		NSUrl MerchantUserAgreementURL { get; set; }

		[Export ("acceptCreditCards")]
		bool AcceptCreditCards { get; set; }

		[Export ("rememberUser")]
		bool RememberUser { get; set; }

		[Export ("languageOrLocale", ArgumentSemantic.Copy)]
		string LanguageOrLocale { get; set; }

		[Export ("disableBlurWhenBackgrounding")]
		bool DisableBlurWhenBackgrounding { get; set; }

		[Export ("forceDefaultsInSandbox")]
		bool ForceDefaultsInSandbox { get; set; }

		[Export ("sandboxUserPassword", ArgumentSemantic.Copy)]
		string SandboxUserPassword { get; set; }

		[Export ("sandboxUserPin", ArgumentSemantic.Copy)]
		string SandboxUserPin { get; set; }
	}

	[Model, BaseType (typeof (NSObject))]
	public partial interface PayPalFuturePaymentDelegate {

		[Export ("payPalFuturePaymentDidCancel:")]
		void DidCancelFuturePayment (PayPalFuturePaymentViewController futurePaymentViewController);

		[Export ("payPalFuturePaymentViewController:didAuthorizeFuturePayment:")]
		void DidAuthorizeFuturePayment (PayPalFuturePaymentViewController futurePaymentViewController, NSDictionary futurePaymentAuthorization);
	}

	[BaseType (typeof (UINavigationController))]
	public partial interface PayPalFuturePaymentViewController {

		[Export ("initWithConfiguration:delegate:")]
		IntPtr Constructor (PayPalConfiguration configuration, PayPalFuturePaymentDelegate futurePaymentDelegate);

		[Export ("futurePaymentDelegate", ArgumentSemantic.Assign)]
		PayPalFuturePaymentDelegate FuturePaymentDelegate { get; }

		[Field ("PayPalEnvironmentProduction", "__Internal")]
		NSString PayPalEnvironmentProduction { get; }

		[Field ("PayPalEnvironmentSandbox", "__Internal")]
		NSString PayPalEnvironmentSandbox { get; }

		[Field ("PayPalEnvironmentNoNetwork", "__Internal")]
		NSString PayPalEnvironmentNoNetwork { get; }	
	}

	[BaseType (typeof (NSObject))]
	public partial interface PayPalPaymentDetails {

		[Export ("shipping", ArgumentSemantic.Copy)]
		NSDecimalNumber Shipping { get; set; }

		[Export ("subtotal", ArgumentSemantic.Copy)]
		NSDecimalNumber Subtotal { get; set; }

		[Export ("tax", ArgumentSemantic.Copy)]
		NSDecimalNumber Tax { get; set; }
	}

	[BaseType (typeof (NSObject))]
	public partial interface PayPalPayment {

		[Export ("paymentWithAmount:currencyCode:shortDescription:intent:")]
		IntPtr Constructor (NSDecimalNumber amount, string currencyCode, string shortDescription, PayPalPaymentIntent intent);

		[Export ("currencyCode", ArgumentSemantic.Copy)]
		string CurrencyCode { get; set; }

		[Export ("amount", ArgumentSemantic.Copy)]
		NSDecimalNumber Amount { get; set; }

		[Export ("shortDescription", ArgumentSemantic.Copy)]
		string ShortDescription { get; set; }

		[Export ("intent")]
		PayPalPaymentIntent Intent { get; set; }

		[Export ("paymentDetails", ArgumentSemantic.Copy)]
		PayPalPaymentDetails PaymentDetails { get; set; }

		[Export ("bnCode", ArgumentSemantic.Copy)]
		string BnCode { get; set; }

		[Export ("processable")]
		bool Processable { get; }

		[Export ("localizedAmountForDisplay", ArgumentSemantic.Copy)]
		string LocalizedAmountForDisplay { get; }

		[Export ("confirmation", ArgumentSemantic.Copy)]
		NSDictionary Confirmation { get; }
	}

	[Model, BaseType (typeof (NSObject))]
	public partial interface PayPalPaymentDelegate {

		[Export ("payPalPaymentDidCancel:")]
		void DidCancelPayment (PayPalPaymentViewController paymentViewController);

		[Export ("payPalPaymentViewController:didCompletePayment:")]
		void DidCompletePayment (PayPalPaymentViewController paymentViewController, PayPalPayment completedPayment);
	}

	[BaseType (typeof (UINavigationController))]
	public partial interface PayPalPaymentViewController
	{

		[Export ("initWithPayment:configuration:delegate:")]	
		IntPtr Constructor (PayPalPayment payment, PayPalConfiguration configuration, PayPalPaymentDelegate paymentDelegate);

		[Export ("paymentDelegate", ArgumentSemantic.Assign)]
		PayPalPaymentDelegate PaymentDelegate { get; }

		[Export ("state")]
		PayPalPaymentViewControllerState State { get; }
	}

	[BaseType (typeof (NSObject))]
	public partial interface PayPalMobile {

		[Static, Export ("initializeWithClientIdsForEnvironments:")]
		void InitializeWithClientIdsForEnvironments (NSDictionary clientIdsForEnvironments);

		[Static, Export ("preconnectWithEnvironment:")]
		void PreconnectWithEnvironment (string environment);

		[Static, Export ("applicationCorrelationIDForEnvironment:")]
		string ApplicationCorrelationIDForEnvironment (string environment);


		[Field ("PayPalEnvironmentProduction", "__Internal")]
		NSString PayPalEnvironmentProduction { get; }

		[Field ("PayPalEnvironmentSandbox", "__Internal")]
		NSString PayPalEnvironmentSandbox { get; }

		[Field ("PayPalEnvironmentNoNetwork", "__Internal")]
		NSString PayPalEnvironmentNoNetwork { get; }	

		[Static, Export ("libraryVersion") /*, Verify ("ObjC method massaged into getter property", "/Users/robert/Downloads/PayPal-iOS-SDK-2.0.3/PayPalMobile/PayPalMobile.h", Line = 69)*/]
		string LibraryVersion { get; }
	}
}
