package 
{
	import com.google.ads.instream.api.Ad;
	import com.google.ads.instream.api.AdErrorEvent;
	import com.google.ads.instream.api.AdEvent;
	import com.google.ads.instream.api.AdLoadedEvent;
	import com.google.ads.instream.api.AdSizeChangedEvent;
	import com.google.ads.instream.api.AdTypes;
	import com.google.ads.instream.api.AdsLoadedEvent;
	import com.google.ads.instream.api.AdsLoader;
	import com.google.ads.instream.api.AdsManager;
	import com.google.ads.instream.api.AdsManagerTypes;
	import com.google.ads.instream.api.AdsRequest;
	import com.google.ads.instream.api.AdsRequestType;
	import com.google.ads.instream.api.CompanionAd;
	import com.google.ads.instream.api.CompanionAdEnvironments;
	import com.google.ads.instream.api.CustomContentAd;
	import com.google.ads.instream.api.FlashAd;
	import com.google.ads.instream.api.FlashAdCustomEvent;
	import com.google.ads.instream.api.FlashAdsManager;
	//import com.google.ads.instream.api.HtmlCompanionAd;
	//import com.google.ads.instream.api.VastVideoAd;
	//import com.google.ads.instream.api.VastWrapper;
	//import com.google.ads.instream.api.VideoAd;
	//import com.google.ads.instream.api.VideoAdsManager;
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.system.Security;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	import flash.text.TextField;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import flash.display.Sprite;
	import flash.display.DisplayObjectContainer;
	import flash.display.DisplayObject;
	import avmplus.FLASH10_FLAGS;
	import flash.text.StyleSheet;
	import flash.text.TextFormat;
	import flash.events.TextEvent;
	import flash.display.Stage;


	public class flashbanner extends Sprite
	{
		private static const CodedBy:String = "Reha Biçer, reha@bilgiparki.com";
		private var adsManager:AdsManager;
		private var adsLoader:AdsLoader;
		private var flashVars:Object;
		private var useGUT:Boolean;
		private static const FALSE:String = "false";
		private var PUBLISHER_ID:String = "ca-games-pub-8917859110938093";
		private var CHANNEL_ID:String = "123";
		private var CHANNEL:String = "Reha";
		private var infoLabel:TextField=new TextField();
		private	var bandLoading:MovieClip = new MovieClip();
		private	var bandBGLoading:MovieClip = new MovieClip();
		private var timer:Timer;
		private static const timeCount:int = 15;

		public var AdsWidth:int;
		public var AdsHeight:int;
		public var AdsStage:MovieClip;

		public function flashbanner()
		{
			Security.allowDomain("pagead2.googlesyndication.com");			
		}

		public function initBanner(_Stage:MovieClip,_URL:String, _Width:int=550, _Height:int=400):void {
			
			AdsWidth = _Width;
			AdsHeight = _Height;
			CHANNEL_ID = _URL;
			AdsStage = _Stage;
			AdsStage.width = AdsWidth;
			AdsStage.height = AdsHeight;
			
			if (AdsStage != null)
			{
				initialize();
				trace('Init...');
			}
			else
			{
				addEventListener(Event.ADDED_TO_STAGE, initialize);
				trace('Add to Stage Init...');
			}
			
		}

		public function disposeBanner() {
			trace("disposeBanner: " );			
			if (adsLoader!=null)
			{				
				adsLoader.req
			}
		}

		private function initialize(event:Event = null):void
		{
			removeEventListener(Event.ADDED_TO_STAGE, initialize);
			loadAd();
		}

		/**
		     * This method is used to create the AdsLoader object if its not present
		     * and request ads using the AdsLoader object.
		     */
		private function loadAd():void
		{
			if (! adsLoader)
			{
				adsLoader = new AdsLoader();
				AdsStage.addChild(adsLoader);
				adsLoader.addEventListener(AdsLoadedEvent.ADS_LOADED, onAdsLoaded);
				adsLoader.addEventListener(AdErrorEvent.AD_ERROR, onAdError);
			}
			adsLoader.requestAds(createAdsRequest());
			trace("Ad requested");
		}

		private function onAdError(adErrorEvent:AdErrorEvent):void
		{
			trace("Ad error : " + adErrorEvent.error.errorMessage);
		}

		// Fires ads has comleted loading an ad
		private function onAdsLoaded(adsLoadedEvent:AdsLoadedEvent):void
		{
			dispatchEvent(new Event("onAdBannerLoaded"));
			
			trace("Ads Loaded");
			adsManager = adsLoadedEvent.adsManager;
			adsManager.addEventListener(AdErrorEvent.AD_ERROR, onAdError);
			adsManager.addEventListener(AdEvent.CONTENT_PAUSE_REQUESTED, onContentPauseRequested);
			adsManager.addEventListener(AdEvent.CONTENT_RESUME_REQUESTED, onContentResumeRequested);
			adsManager.addEventListener(AdLoadedEvent.LOADED, onAdLoaded);
			adsManager.addEventListener(AdEvent.STARTED, onAdStarted);
			adsManager.addEventListener(AdEvent.CLICK, onAdClicked);
			adsManager.addEventListener(AdEvent.USER_CLOSE, onAdClosed);

			if (adsManager.type == AdsManagerTypes.FLASH)
			{
				var flashAdsManager:FlashAdsManager = adsManager as FlashAdsManager;
				flashAdsManager.addEventListener(AdSizeChangedEvent.SIZE_CHANGED, onFlashAdSizeChanged);
				flashAdsManager.addEventListener(FlashAdCustomEvent.CUSTOM_EVENT, onFlashAdCustomEvent);
				flashAdsManager.x = 0;
				flashAdsManager.y = 0;
				//flashAdsManager.adSlotWidth = AdsStage.stageWidth;
				//flashAdsManager.adSlotHeight = AdsStage.stageHeight;
				flashAdsManager.adSlotWidth = AdsWidth;
				flashAdsManager.adSlotHeight = AdsHeight;
				flashAdsManager.load();
				flashAdsManager.play();

				trace("Calling load, then play");
			}


		}

		private function createAdsRequest():AdsRequest
		{
			var request:AdsRequest = new AdsRequest();
			request.adSlotWidth = AdsWidth;
			request.adSlotHeight = AdsHeight;
			request.contentId = CHANNEL_ID;
			request.adTagUrl = '';
			request.adType = AdsRequestType.FULL_SLOT;
			request.channels = CHANNEL.split(",");
			request.publisherId = PUBLISHER_ID;

			//request.disableCompanionAds = true;
			request.maxTotalAdDuration = 5;

			// Checks the companion type from flashVars to decides whether to use GUT
			// or getCompanionAds() to load companions.
			useGUT = flashVars != null && flashVars.useGUT == FALSE ? false:true;
			if (! useGUT)
			{
				request.disableCompanionAds = true;
			}
			return request;
		}

		private function onContentPauseRequested(event:AdEvent):void
		{
			trace(event.type);
		}


		private function onContentResumeRequested(event:AdEvent):void
		{
			trace(event.type);
		}

		private function onAdStarted(event:AdEvent):void
		{
			dispatchEvent(new Event("onAdStarted"));
			trace(event.type);
		}

		private function onAdClicked(event:AdEvent):void
		{
			dispatchEvent(new Event("onAdClicked"));
			trace(event.type);
		}

		private function onAdLoaded(event:AdLoadedEvent):void
		{
			trace(event.type);
			trace(sortChildren(stage));
		}

		private function onAdClosed(event:AdEvent):void
		{
			disposeBanner();
			dispatchEvent(new Event("onAdClosed"));
			trace(event.type);
		}

		private function onFlashAdSizeChanged(event:AdSizeChangedEvent):void
		{
			trace(event.type);
		}

		private function onFlashAdCustomEvent(event:FlashAdCustomEvent):void
		{
			trace(event.type);
		}

		public static function sortChildren( container:DisplayObjectContainer):Boolean
		{

			var numChildren:int = container.numChildren;
			//no need to sort (zero or one child)
			if ( numChildren < 2 )
			{
				return false;
			}

			var depthsSwapped:Boolean;

			//create an Array to sort children
			var children:Array = new Array(numChildren);
			var i:int = -1;
			while ( ++i < numChildren )
			{
				children[i] = container.getChildAt(i);
			}

			//sort by children's y position
			children.sortOn( "y", Array.NUMERIC );

			var child:DisplayObject;
			i = -1;
			while ( ++i < numChildren )
			{
				child = DisplayObject(children[i]);
				//only set new depth if necessary
				if ( i != container.getChildIndex( child ) )
				{
					depthsSwapped = true;
					//set their new position
					container.setChildIndex( child, i );
				}

			}

			//returns true if 1 or more swaps were made, false otherwise
			return depthsSwapped;

		}

	}// Banner



}