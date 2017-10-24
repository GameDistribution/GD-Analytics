		public static function LoadBanner():void
		{						
			Security.allowDomain("*")
			var context:LoaderContext = new LoaderContext(false, new ApplicationDomain(ApplicationDomain.currentDomain),SecurityDomain.currentDomain); // Use For Running
			//var context:LoaderContext = new LoaderContext(false, new ApplicationDomain()); // Use For Debugging
			context.allowCodeImport = true;
			var loader:Loader = new Loader();
			loader.contentLoaderInfo.addEventListener(Event.COMPLETE, ADSonLoaderComplete);			
			loader.contentLoaderInfo.addEventListener(Event.INIT, ADSinitHandler);
			loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, ADSioErrorHandler);
			loader.load(new URLRequest("http://6a9feab0-4e67-47e5-a19b-d98fafd6850c.s1.submityourgame.com/ads/ads.swf"), context);				
		
		}
				
		internal static function ADSonLoaderComplete(event:Event):void {
			var loaderInfo:LoaderInfo = event.target as LoaderInfo;
			var swf:Class = (event.target as LoaderInfo).applicationDomain.getDefinition("flashbanner") as Class;
			var app:Object = new swf();
			
			app.addEventListener("onAdClosed",onAdClosed);
			
			adsBannerArea = new MovieClip();
			adsBannerArea.width = _root.stage.width;
			adsBannerArea.height = _root.stage.height;
			
			adsBannerArea.addChild ( DisplayObject(app) );
			
			_root.stage.addChild(adsBannerArea);
			
			app.initBanner(adsBannerArea,WebRef,_root.stage.stageWidth,_root.stage.stageHeight);		
		}
		
		internal static function onAdClosed(event:Event):void {
			trace("FGSAPI: onAdClosed "+adsBannerArea.numChildren);
			
		}
		
		internal static function ADSinitHandler(event:Event):void {
			var loader:Loader = Loader(event.target.loader);
			var info:LoaderInfo = LoaderInfo(loader.contentLoaderInfo);
			trace("initHandler: loaderURL=" + info.loaderURL + " url=" + info.url);
		}
		
		internal static function ADSioErrorHandler(event:IOErrorEvent):void {
			trace("ioErrorHandler: " + event);
		}		
