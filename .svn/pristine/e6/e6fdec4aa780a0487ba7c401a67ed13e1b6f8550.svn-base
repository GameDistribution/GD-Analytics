package 
{
	import com.google.ads.instream.api.AdsRequest;
	import com.google.ads.instream.api.AdsRequestType;
	import com.google.ads.instream.api.AdsLoader;
	import com.google.ads.instream.api.AdsLoadedEvent;
	import com.google.ads.instream.api.AdErrorEvent;
	import com.google.ads.instream.api.AdsManager;
	import com.google.ads.instream.api.AdEvent;
	import com.google.ads.instream.api.AdsManagerTypes;	
	import com.google.ads.instream.api.VideoAdsManager;	
 	import com.google.ads.instream.api.FlashAdsManager
 
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.system.Security;
	import flash.display.Sprite;
	import flash.display.DisplayObject;
	import flash.display.Stage;
	import flash.display.DisplayObjectContainer;


	public class flashbanner extends Sprite
	{
		private static const CodedBy:String = "Reha Biçer, reha@bilgiparki.com";
		private var adsManager:AdsManager;
		private var adsLoader:AdsLoader;
		private var PUBLISHER_ID:String = "ca-games-pub-8917859110938093";
		private var CHANNEL_ID:String = "123";
		private var CHANNEL:String = "Reha";
		public var AdsWidth:int;
		public var AdsHeight:int;
		public var AdsStage:MovieClip;

		public function flashbanner()
		{
			Security.allowDomain("*");	
			//initBanner(stage,'',500,400);
		}

		public function initBanner(_Stage:MovieClip,_URL:String, _Width:int=550, _Height:int=400):void {
			
			AdsWidth = _Width;
			AdsHeight = _Height;
			CHANNEL_ID = _URL;
			AdsStage = _Stage;
			//AdsStage.width = AdsWidth;
			//AdsStage.height = AdsHeight;
			
			if (AdsStage != null)
			{
				loadAd();
				trace('Init...');
			}
			
		}

		public function disposeBanner() {
			trace("disposeBanner: " );			
		}

		private function loadAd():void
		{
			if (! adsLoader)
			{
				// Create AdsLoader
				var adsLoader:AdsLoader = new AdsLoader();
				AdsStage.addChild(adsLoader);
				
				//Add an event listener
				adsLoader.addEventListener(AdsLoadedEvent.ADS_LOADED, onAdsLoaded);
				adsLoader.addEventListener(AdErrorEvent.AD_ERROR, onAdError);
				 
				// Create AFG (AdSense for Games) AdsRequest
				var adsRequestAFG:AdsRequest = new AdsRequest();
				adsRequestAFG.adSlotWidth = AdsWidth;
				adsRequestAFG.adSlotHeight = AdsHeight;
				adsRequestAFG.publisherId= PUBLISHER_ID;
				adsRequestAFG.adType = AdsRequestType.FULL_SLOT;
				adsRequestAFG.channels = ["FGS"];
				adsRequestAFG.contentId = "1";
				adsRequestAFG.maxTotalAdDuration = 15;
				
				
				// Request AFG Ad (uncomment below line to use)
				adsLoader.requestAds(adsRequestAFG);
				
			}
			trace("Ad requested");
		}

		private function onAdError(adErrorEvent:AdErrorEvent):void
		{
			trace("adErrorEvent" + adErrorEvent.error.errorMessage);
		}


		private function onAdsLoaded(adsLoadedEvent:AdsLoadedEvent):void {
		  // Get AdsManager
		  adsManager = adsLoadedEvent.adsManager;
		  adsManager.addEventListener(AdErrorEvent.AD_ERROR, onAdError);
		
		  // Listen and response to events which require you to pause/resume content
		  adsManager.addEventListener(AdEvent.CONTENT_PAUSE_REQUESTED, onPauseRequested);
		  adsManager.addEventListener(AdEvent.CONTENT_RESUME_REQUESTED, onResumeRequested);
		  adsManager.addEventListener(AdEvent.USER_CLOSE, onAdClosed);
		  adsManager.addEventListener("AdStopped", onAdClosed);
		
		  if (adsManager.type == AdsManagerTypes.FLASH) {
			var flashAdsManager:FlashAdsManager = adsManager as FlashAdsManager;
			flashAdsManager.x = 0;
			flashAdsManager.y = 0;
			flashAdsManager.adSlotWidth = AdsWidth;
			flashAdsManager.adSlotHeight = AdsHeight;
			flashAdsManager.load();
			flashAdsManager.play();
		  }
		 }

		private function onPauseRequested(event:AdEvent):void
		{
			trace("onPauseRequested",event.type);			
		}

		private function onResumeRequested(event:AdEvent):void
		{
			trace("onResumeRequested",event.type);
		}

		private function onAdStarted(event:AdEvent):void
		{
			dispatchEvent(new Event("onAdStarted"));
			trace("onAdStarted",event.type);
		}

		private function onAdClicked(event:AdEvent):void
		{
			dispatchEvent(new Event("onAdClicked"));
			trace("onAdClicked",event.type);
		}


		private function onAdClosed(event:AdEvent):void
		{
			dispatchEvent(new Event("onAdClosed"));
			trace("onAdClosed",event.type);
			if (adsManager) {
			 //Remove listeners that were attached to adsManager
			 try {
				 adsManager.unload();
			 } catch (e:Error) {
			 
			 }
			 //adsManager = null;
			}
		}

	}// Banner



}