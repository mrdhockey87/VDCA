﻿using ObjCRuntime;
using UIKit;
//using VDCA.iOS;

namespace VDCA;

public class Program
{
	// This is the main entry point of the application.
	static void Main(string[] args)
	{
        //DependencyService.Register<TextMeasurement_iOS>();
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
	}
}
