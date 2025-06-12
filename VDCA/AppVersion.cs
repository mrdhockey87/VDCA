namespace VDCA
{
    public static class AppVersion
    {
        public static string AppVersionNo {
            get
            {
                return Constants.APP_VERSION;
            }
        }
        public static string AppBuildNo
        {
            get
            {
                return Constants.APP_BUILD;
            }
        }
    }
}

/*
 * 
 *  v 6.038.0555 Fixed the MenuBar for Mac & Windows, it now works and the commands work. Fixed the About screen to fit
 *               all screen sizes. Set it so the flyout menu does not show on mac or windows. Updated Nugets. Fix it so
 *               the Database version & copyright get updated even if the database is copied and on first run. Added the 
 *               Progress Service to handle the progressbar showing & hiding in the app shell. Fixed the license view tiltles mdail 6-11-25
 *  v 6.038.0554 Set trying to get the menu bar to work for Apple & WInUI, however the Commands don't work, I got the 
 *               flyout menu back though. mdal 6-11-25
 *  v 6.038.0553 Trying to get the menuBar to work for Apple & WInUI, however it removes the flyout when it is there. mdail 6-10-25
 *  v 6.038.0552 Remove 32 bit cpu's from android build option as it crashes on andorid 32 bit cpu's mdail 6-5-25
 *  v 6.038.0551 Fixed app to properly run on all types of cpu's for android ios and mac mdail 6-525
 *  v 6.037.0550 Fix the copyright information missing when run for the first time on a device mdail 6-4-25
 *  v 6.037.0549 Added About screen to the flyout menu and added the AppVersion & Build to the About screen. mdail 6-3-25
 *  v 6.037.0548 Fix the license text files, remove unneeded content mdail 5-30-25
 *  v 6.037.0547 Fix the Apache License text still having some line feeds in it and commit a file that was missing from the last commit. mdail 5-30-25
 *  v 6.037.0546 Fixed the LicenseView get the license file based on the package name so it updates the proper copyright. Added 2 boxes
 *               to the LicenseView to get the border. mdail 6-3-25
 *  v 6.037.0545 For some reason it is reading the wrong info for the last SQLitePCLRaw.bundle_sqlcipher version and name mdail 5-29-25
 *  v 6.037.0544 Fixed the background & text color of the LicenseView and added a border around the License text. Removed the
 *               Progress Overlay from the LicenseView as it wasn't needed. mdail 5-29-25
 *  v 6.037.0543 Added the logic to display a list of 3rd party licenses and the actual license when selected mdail 5-23-25
 *  v 6.036.0542 Fixed the review quiz saving problem by using a batch async transaction on the database to save the quiz data. mdail 5-15-25
 *  v 6.036.0541 Made it so the rewiew quiz save in real time, I think I need to make it so the review quiz data has it's own database
 *               that isn't encrypted so that it doesn't slow the app down so much. mdail 5-14-25
 *  v 6.036.0540 Made it so saving the review quiz question works in the background so it doesn't slow the app down so bad. mdail 5-14-25
 *  v 6.036.0539 Update Nugets to latest version. mdail 5-14-25
 *  v 6.036.0538 Fix Maccatalyst to the correct target version because it wasn't the correct mimimum version. mdail 5-13-25
 *  v 6.036.0537 Test for Gittyup interface for github mdail 5-9-25
 *  v 6.036.0536 I had to remove one nuget to get it to run on iOS, I also lowered the Explaination and Citation icon on the
 *               flashcard to improve appeariance. I fixed the version number and copywrite line on the main page. mdail 5-8-25
 *  v 6.036.0535 The VersionInfo VersionString gets set however the main page label does not update properly mdail 5-7-25
 *  v 6.036.0533 Try setting the database version number and the rest of the version string in the DbVersion CheckVersionNo method by setting a
 *               public string VersionLabelString in the MainPage. mdail 5-6-25
 *  v 6.036.0532 Added code to check the database that is install on the device to see if it is encrypted ot not and if not copy the new database
 *               into place. I had to change the copy database code to use FileStream CopyToAsync, and added await to the using statements to wait
 *               for the dispose to make sure that there isn't a file left open. mdail 5-6-25
 *  v 6.036.0531 Updated the Sqlite db to the encrypted version. mdail 5-5-25
 *  v 6.035.0530 FIxed the ApplicationVersion & the PackageVersion, set them to 6 & 6.0.0 to match the ApplicationDisplayVersion better. mdail 4-29-25
 *  v 6.035.0529 Update Nugets and test mdail 4-23-25
 *  v 6.035.0528 Commit to add the On Navigating event to the AppShell mdail 4-23-25
 *  v 6.035.0527 Fixed bug that showed up with the overlay's and screen rotation on the main page. mdail 4-9-25
 *  v 6.035.0526 Added hide progress bar to the OnSizeChanged method in the MainPage to hide the progress bar when the screen rotates. mdail 4-9-25
 *  v 6.035.0525 Added the help view to the flashcard, practice & quiz. Added an optional variable to the help page to set the page 
 *               to hide the hamburger icon and the label and set row 0 to height 0 in the card views. mdail 4-9-25
 *  v 6.035.0524 Fixed it so the Help page uses the same code as the HelpContentView which used to be named the MainHelp by adding an
 *               event that the HelpPage can subscribe to that closes the page, if the event isn't subscribe then it just uses the
 *               exit code in the help content view mdail 4-9-25
 *  v 6.035.0523 Fix the help pages background and text colors mdail 4-8-25
 *  v 6.035.0522 Had to make the HelpPage different from the MainHelp view because I needed the exit button to do 
 *               different things . mdail 4-8-25
 *  v 6.035.0521 Fix errors in the text of the help screen and add the help screen to the flyout menu. mdail 4-4-25
 *  v 6.035.0520 Started to add the help - onboarding page to the app. I removed the old OnboardingSupport from the 
 *               main pages mdail 4-4-25
 *  v 6.035.0519 Added android:hardwareAccelerated="false" because the menu icons cause the app to crash on Android. mdail 4-4-25
 *  v 6.035.0518 Got all the Icons for the menu added and changed the hidden icon to big_eye & unbig_eye mdail 4-3-25
 *  v 6.035.0517 Started adding menu icon to flyout menu with the feedback icon, downloaded others need to make them they way I want then, impoert tem into 
 *               the porject and cahnge and check then in the flyout menu. mdail 4-2-25
 *  v 6.035.0516 Changed if (navigationStack.Count > 0) to if (navigationStack.Count > 1) to fix ShowCustomModalAsync. as navigationStack[0] is what was null
 *               and causing the error. I am not sure what changed to make the difference however it works now. mdail 4-2-25
 *  v 6.035.0515 Now that I have the menu looking the way I want it to, whem trying to display the alert Page it is not working and not
 *               giving me a clear reason, it is a null exception on await navigationStack[navigationStack.Count - 1].Navigation.PushModalAsync(page, false)
 *               which isn't making a lot of sence to me. mdail 4-1-25
 *  v 6.035.0514 Change it so the icons show up after the text i the menu part of the flyout menu. I couldn't get the alignment to work
 *               the way I wanted so they are right after the text in the menu. I also get the Under line to show up after the Header mdail 3-31-25
 *  v 6.035.0513 Renamed the Folder that was mistyped and moved repositry to GitHub mdail 3-27-25
 *  v 6.035.0512 Fixed menu item missing on flyout menu by removing the datatype mdail 3-26-25
 *  v 6.035.0511 Added the await for the delay in the OnSizeChanged in the CardView to fix the problem with the card losing position
 *               in Android when the screen rotates. mdail 3-26-25
 *  v 6.035.0510 Android on rotation the card scrolls back one at least, not to fix and check mode, also change the instruction to
 *               be simply Select the correct answer for the quiz & Practice mdail 3-25-25
 *  v 6.035.0509 I had to add ExplanationVisible & CitationVisible properties to the CardView to get the explanation and citation 
 *               to work and not complain about the binding. mdail 3-25-25
 *  v 6.035.0508 Added a call to ResetScrollToTop in the OnSizeChanged so that when the screen rotates the card is scrolled
 *               to the top if it isn't currently mdail 3-25-25
 *  v 6.035.0507 Added IsCVisible & IsDVisible to the Question model to make it easier to hide the C & D answers in the
 *               Practice & Quiz views. 
 *  v 6.035.0506 Clean up warning messages, added async task methods to add the selected categories to the list and to 
 *               deselect categories. Also changed 2 Dispatcher.Dispatch to MainThread.InvokeOnMainThreadAsync. Both of these
 *               changes were because it sayed there was no await in the affected methods. mdail 3-21-25
 *  v 6.035.0505 Refactored the OnPositionChanged in the card view to do each of the operations as seperate methods to make it 
 *               easier to read mdail 3-19-25
 *  v 6.035.0504 Fixed the labels above the collection view in the select view mdail 3-20-25
 *  v 6.035.0503 Added a bindable property for the GridMargin to the SelectViewModel and set it in the SelectView to fix the grid in 
 *               portrait mode the first time the screen rotates from landscape to portrait mdail 3-19-25
 *  v 6.035.0502 Update to the latest version of visual studio 2022 mdail 3-15-25
 *  v 6.035.0501 Tring to fix the select view losing the left margin on rotation on android mdail 3-1 4-24
 *  v 6.035.0500 Update Nugets to the latest version, had to set ItemsSource to null then reset it, then reposition it on screen rotation
 *               because if I didn't if the question on the screen had 4 answer and one of the close ones only had 2 it would push
 *               the 2 off the top of the screen and not add the scroll either, then I had to reset the scroll bar at the top as it 
 *               lost track of the position. If I did the same code on iOS it would crash the app with an index out of range error .mdail 3-13-25
 *  v 6.035.0499 Fixed the review question, it now shows the correct score, color, etc. I also changed the date to use / instead of - mdail 3-13-25
 *  v 6.035.0498 Trying to fix the Review Quiz Question (which is the review quiz after taking to quiz from the quiz view) not updating 
 *               and showing the data properly. Changed the View model and changed the binging in  the ReviewQuestion.cs mdail 3-13-25
 *  v 6.035.0497 added ReloadCurrentItem to the init of the practice card so it fills the screen as it should, without itit sometimes
 *               only fills half the screen. mdail 3-13-25
 *  v 6.035.0496 After uploading to Google I found that the app is crashing on Android changing pages to the cards for some reason.
 *               So after looking up one of the errors I changed the private methods to internal for the cards mdail 3-13-25
 *  v 5.035.0495 I removed the RefreshView, and changed the ScrollView to VerticalOptions="FillAndExpand" in the Practice & Quiz views
 *               which alone with some of the other changes seems to have fixed the display staying the correct size when the screen rotates.
 *               I also was able to set the Loop to false which seems to work without crashing, the Loop being set to Fasle was crashing when
 *               the screen tried to load, It is supposed to be False, I don't know it false is why it works now. mdail 3-7-25
 *  v 5.035.0494 Back to having intermittent problem with the scrollview getting cut of an not being tall enough on the Practice & 
 *               Quiz views, even if I add to the height of the scrollview it still gets cut off and does not change. mdail 3-6-25
 *  v 5.035.0493 I added a 1 millisecond delay before moving back to the original position for iOS & MacCatalyst in the OnSizeChanged 
 *               method to keep the corret position when the screen rotates. mdail 3-6-25
 *  v 5.035.0492 If I try to fix iOS losing the position when the screen rotates it causes the cards to jump around when the screen 
 *               rotates, the way it is it jumps backward when rotating to landscape and forward when rotating to portrait. If you rotate
 *               it full circle it will end up in the same place it started. mdail 3-5-25
 *  v 5.035.0491 Changed OnScrolled so it only adjust the position for Android and WinUI as when it did it for iOS & MacCatalyst. It
 *               was causing the position to jump from 4 (Portrait) to 2 (Landscape)  to 8 (Portrait)and then carzy form there. Now
 *               it only goes back and forth from  4 (Portrait) to 2 (Landscape), and the adjustment in OnSize seem to be egnored.
 *               Also tried setting the border smaller for iOS however it setill getts bigger above and below the questions. mdail 3-3-25
 *  v 5.035.0490 For some reason, the CarouselView does not updating the position properly when when the orientation changes at 
 *               least on iOS mdail 2-28-25
 *  v 5.035.0489 FIxed the rotation locking up by adding ios:Page.UseSafeArea="True" to the select 
 *  v 5.035.0488 Now I need to figure out why the app is locking up when the screen rotates in the Selection for Flash & Practice views 
 *               And it does it in both CollectionViewHandler2 & the original mdail 2-27-25
 *  v 5.035.0487 I had to set it so it doesn't try to fix the scroll position on Windows as there is a problem getting the VisibleView on Windows
 *               so it doesn't work. mdail 2-27-25
 *  v 5.035.0486 In the OnPossitionChanged for the Practice & Quiz Views I had to find the previous & current view then scroll both views to
 *               the top to make it so if you scrolled 2 card in a row then went back to the first, then to the second it would scroll both
 *               of them to the top, without scrolling both to the top the second one stayed scrolled. mdail 2-26-25
 *  v 5.035.0485 Trying to figure out how to scroll to the top of a card if it was previously scrolled down it locks up. WHen I tru to hit break
 *               it says there is not code running. mdail 2-26-25
 *  v 5.035.0484 Using CollectionViewHandler2 & CarouselViewHandler2 finally fixed the problem with the cards in the Practice & Quiz views and
 *               is a little faster on the lists and the cards. Also simplified the Practice & Quiz layouts a little. mdail 2-24-25
 *  v 5.034.0483 Added VerticalOptions="Start" to the top grid in the practice view to help the layout.It still isn't working despite trying
 *               many option, I removed the ScrollView, Border and made other changes and it still isn't working. Mdail 2-13-25
 *  v 5.034.0482 Still trying to fix the top of the Border being too high on iOS, tried changing marin in ExicuteRefreshCommand in the CardView last mdail 2-12-25
 *  v 5.034.0481 Set the margin for the cards to add 20 to the top for iOS & MacCatalyst to fix the card being place to high mdail 2-18-24
 *  v 5.034.0480 Need to fix the top of the Border being too high on iOS, need to make sure the place the letters were isn't being used by
 *               blank space also in a;ll the cards. mdail 2-10-25
 *  v 5.034.0479 Switching back to CarouselViewHandler fixed the problem with the cards in the Practice & Quiz views, however it is still slow
 *               as I can't use the CollectionViewHandler2. I can set Loop to false for the CarouselView. mdail 2-10-25
 *  v 5.034.0478 Update Nugets see if that fixes the problem with the cards in the Practice & Quiz views mdail 2-10-25
 *  v 5.034.0475 Tried to fix the way the cards are getting shortend randomly, it will only show half the screen however the top part will 
 *               scroll so the amswers can be seen but they aren't on the screen without scrolling, 1/2 the screen is empty. I don't know
 *               how to fix it. I tried reloading the questions and invaldating the layout and using the refresh command, none of them worked. mdail 1-17-25
 *  v 5.034.0474 Moved the OnPositionChange to the CardView Class to try and figure out how to stop loop in codebehind mdail 1-16-25
 *  v 5.034.0473 Switching to CarouselViewHandler2 fixed the problem in iOS & MacCatalyst with the cards layout and does see to be a little 
 *               quicker. I tryed CollectionViewHandler2 however it takes 25 seconds or more to load the Select lists so I switch back to
 *               not using it for now. However I had to set Loop to true for the CarouselView's which means that the user can loop back to the
 *               end of the cards by swiping. The buttons still say your at the first or last card. mdail 1-16-25
 *  v 5.033.0472 While trying to fix the cards in practice & quiz another update to the nugets came along, nothing had fixed it yet so I removed
 *               all the code except for the PageGrid name and passing the name to the CardView. mdail 1-16-25
 *  v 5.033.0471 Found a site to remove the background from the red loading gifs after I found a way to make them gif from lotties mdail 1-15-25
 *  v 5.033.0470 To reduce duplicate code I made a class in the Utils directory to show and hide the progress overlay with it short delay. mdail 1-14-25
 *  v 5.033.0469 Set it so showing the progress was async with an await so that it would show the progress before the next step was done mdail 1-14-25
 *  v 5.033.0468 Had to add using statment to all files as the latest version of MAUI has changed the way it handles the using 
 *               statments or something, it seem that the universal using's are no longer used mdail 1-13-23
 *  v 5.033.0467 Changed the progrss bar overlay to use the gif file of the lottie files as the lottie files
 *               were not showing properly. Also fixed a bug in the quiz when the long quiz was selected it 
 *               was giving a null pointer exception on the sort, I change it to return the pip fix as a list instead of
 *               doing it inline. mdail 1-10-25
 *  v 5.033.0466 Tried to make the progress animation work smother however it keeps hanging mdail 1-10-25
 *  v 5.033.0465 Remove the fade in animation for the progress bar overlay because it was to slow and not needed mdail 1-10-25
 *  v 5.033.0464 Changed the progress overlay for using the progressbar to use lottie files, one of two depending on the platform. mdail 1-7-25
 *  v 5.033.0463 Removed the condition check in the on size changed for the card view as in mac at least the size can change without the orinantion 
 *               changing. I need to figure out how to get the progress overlay to be larger on the MacCatalyst. Also I need to figure out why
 *               the title isn't showing in the practice card at least in MacCatalyst. mdail 1-7-25
 *  v 5.033.0462 Had to change Support Platform for MacCatalyst to 15.0 as it was crashing when trying to run on the MacCatalyst. Added title
 *               Constants for the Practice, Quiz Review Quiz Question and Flashcard and set the Title for Practice as it wasn't being set. mdail 1-7-25
 *  v 5.033.0461 Somewhere alone the line MAUI has changed to not allow negative padding in at least some cases in the app whne it is run in 
 *               windows as the letter Q: needed a -6 for one of Bottom and having it there caused the cards not to work in Practice & Quiz.
 *               Removing it has now fixed the problem. mdail 1-6-25
 *  v 5.033.0460 FIx the binding error reported by Live Visual Tree in the FlashcardView, PracticeCardView & QuizView however the review still
 *               has some and the windows version of the Practice and Quiz don't show the cardsmdail 1/3/25
 *  v 5.033.0459 Updated Questions model to be in line with naming conversions and to try to elimiate null in the shuffled answers
 *               also shortend the init of some collection in the QuestionDatabase class mdail 1/3/25
 *  v 5.033.0458 Set iOS minimum to 13.0, after updating the nuget using the Datatype in Android no longer crashes when not 
 *               running in the debugger. however the practice & quiz are not showing at all in windows now. mdail 1/3/25
 *  v 5.033.0457 Update to newest nugets mdail 1/3/25
 *  v 5.033.0456 After getting the App to actually start on windows, I found that on Android is would crash when the Practice or Quiz views were 
 *               loaded when run without being in the debugger, I finally got that fixed yet I don't know if it will work on Windows as it was
 *               simply not displaying the card in Practice or Quiz view. For some reason the Datatype caused the app to crash when not in Debug mdail 12-10-24
 *  v 5.033.0455 Try to clean out project file of all the duplicat entries I found trying to get it to work on net 9.0 the first time mdail 12-9-24
 *  v 5.033.0454 Backed the app to Net 8.0 and fixed some issues that had come up trying to set datatypes to make sure it runs. mdail 12-9-24
 *  v 5.033.0453 Changed the images infront of the Question and Answer to label with tht letters Q ,A,B,C & D in the FlashcardView, 
 *               PracticeCardView as it looks a little better I think mdail 12-2-24
 *  v 5.033.0452 Fixed swipping by setting the scrollThreshold to 0.3 in the CardView so that it scrolls when the user swipes the card and moves
 *               it 30% of the way to the next/previous card. It is still a little slow when the card first loads, however it does eventually
 *               scroll to the next card & the delay is not too teriablly bad. mdail 11-25-24
 *  v 5.033.0451 Working on improving the scrolling when swiping the cards in the FlashcardView, PracticeCardView & QuizView. I have added a
 *               the OnScrolled to the CardView so I can track the scrolling and see if it is right & left as it is supposed to, mdail 11-25-24
 *  v 5.033.0450 Remove asking to verify the choice in the Practice card mdail 11-25-24
 *  v 5.033.0449 Removed the buttons at the top of the Practice & Quiz views and removed the references to the Button Images & Button Backgrounds
 *               in the question model and in the Instruction label at the top of the cards. It all need to be texsted more I did a short test on the practice view mdail 11-22-24
 *  v 5.033.0448 Fix a couple of sizes that were wrong in the FLashcards, PracticeCards & QuizCards the Question FontSize was too big and the
 *               the Q in front was to small, then too big, so I had to make a new size for th eChevrons at the bottom of the cards. mdail 11-22-24
 *  v 5.033.0447 Fix card index changing when screen rotated landscape to portrait mdail 11-21-24
 *  v 5.033.0446 Fix cards not centering when screen rotated into landscape mdail 11-20-24
 *  v 5.033.0445 Updated the nugets to the latest version. mdail 11-20-24
 *  v 5.033.0444 Changed the Check mark to a thicker one and made it so the color match the Checkbox where the background is filled in white
 *               and the check mark is yellow when it is selected and the backgound id transparent and the border is navy when it is not mdail 11-13-24
 *  v 5.033.0443 Work on fixing the SupportReguiredThisPage so it give the correct decision mdail 11-12-24
 *  v 5.033.0442 Continue to work on the OnboardingSupportHelp class, I added the GetVersions method to get the CurrentVersion & 
 *               PreviousVersion and a way to reset the saved preferences for each page when the version changes. I changed the Arrow
 *               images in the resources folder to better looking smaller arrows I made. Need to rework the ShouldShowOnboarding method
 *               to set the Constants ShowOnboardingSupportMain, ShowOnboardingSupportFlash, ShowOnboardingSupportPractice &  
 *               ShowOnboardingSupportQuiz at startup tthen check in each page mdail 11-1-24
 *  v 5.033.0441 Continued with the OnboardingOverlayControl and the NavigationOnboardingOverlayControl to the CustomControl directory. I added
 *               the code to make it so the total pages can be set from the underlying page and the dots will be generated and the current page 
 *               will be highlighted. I also added the code to make the next and previous buttons work. Thw nwxt & previous buttons also raise an
 *               event in the underlying page so I can change the number & placement & visibility of ArrowCaption views from the underlying page.
 *               I also added borders around the letter button in the ParcticeCard & Flashcard views to make them look better. I still need to 
 *               add onboard show/don't show methods for the mainpage, flashcards, practice & quiz views seperatly. Right now I only have one way
 *               which would mean when the user set don't show this again it would keep the others from showing. mdail 10-30-24
 *  v 5.033.0440 Added the OnboardingOverlayControl and the NavigationOnboardingOverlayControl to the CustomControl directory. THese are used
 *               to make the Onboarding Support pages. I still have to figure out exacatly how to locate them on the screen and control the 
 *               navigation to the next page. I also need to figure out how to make the arrow point to the correct location. mdail 10-29-24
 *  v 5.033.0439 Got the Checkbox contreol to work with the pasted in Check (✓) from the icon font, I changed the size of the box to 28 and
 *               changed the margins to make everything fix better. mdail 10-29-24
 *  v 5.033.0438 Trying to ge tthe ✓ to work form icon fonts and they don't seem to be working, I need to figure out why. The fonts
 *               work with lower numbered icons just not the higher ones. Using the check in Text field seems to work though. mdail 10-28-24
 *  v 5.033.0437 Updated nugets to the latest version. mdail 10-25-24
 *  v 5.033.0436 I had to change the CustomCheckbox as the last one did not work as expected, also the App crashed when I tried to run it on Windows
 *               I don't know if that is releated, when I tried to run it on Android it hung, however it ran without the debugger? mdail 10-25-24
 *  v 5.033.0435 More work on the OnboardingSupportHelp class, I also add a CustomCheckBox class to the CustomControls directory to try and fix the
 *               Checkbox bug and use it to replace the Checkbox in the SelectFlashView & SelectPracticeView. I added it to the XAML in the SelectFlashView
 *               & the SelectPracticeView it need to be tested on all platforms. mdail 10-24-24
 *  v 5.033.0434 Start working on adding Onboarding Support pages for the app. I have added the OnboardingSupportHelp class to the Utils
 *               and started adding the code I need to finish adding more from the notes for the AI's in Notepad++. I need to fix
 *               the Left arrow for the support pages It is too small. mdail 10-23-24 
 *  v 5.033.0433 Fix app crash due to null pointer exception when trying to set ask gone mdail 10-18-24
 *  v 5.033.0432 Changed the app start up to make sure that the check version is finished before it loads the data so if the database
 *               has been updated it will load the data properly. After testing 2 version of the SlectFlashView & SelectPracticeView layouts
 *               that should have been simpler I found the were slower than the original layouts so I reverted them back to the original mdail 10-18-24
 *  v 5.033.0431 Tried converting the layout of the select to use stacklayout instead of grid, I think it slowed the loading down
 *               I saved the newer version of the layouts in the "_VDCA Images and other stuff" directory, needs more testiing mdail 10-26-24
 *  v 5.033.0430 Converted back to using the checkbox's as the images slowed down the loading of the SelectFlashView & SelectPracticeView and
 *               the select all & deselect all buttons. I also trimmed as much code as i can find that might cause double exicution of methods
 *               to try to speed up the loading of the SelectFlashView & SelectPracticeView and the select all & deselect all buttons. mdail 10-16-24
 *  v 5.033.0429 Added await MainThread.InvokeOnMainThreadAsync to Select & Deselect all to see if that would speed 
 *               anything up no changed mdail 10-25-24
 *  v 5.033.0428 Fixed some of the classes missing the partial keyword, I tried converting the select flashcard & practice views to not 
 *               use the base class and view model, however it didn't make any speed difference so I reverted it back to the base 
 *               class and view model mdail 10-15-24
 *  v 5.033.0427 The latest updates finnally fix the CommunityToolkit.Maui so I could update it to 9.0.10 mdail 10-14-24
 *  v 5.033.0426 I finally got VS Code to the point I can debug & run the app on the MacCatalyst and iOS Simulators. mdail 10-14-24
 *  v 5.033.0425 Removed the interfaces directory from the project file mdail 10-10-24
 *  v 5.033.0424 Changed the Splash screen background color to Power Blue mdail 10-7-24
 *  v 5.033.0423 NOTE: as of right now if I update the Nugets it will not work. It's one of the three Nugets Microsoft.Maui.Controls.Compablilty 8.0.91
 *               Microsoft.Maui.Controls 8.0.91 or CommunityToolkit.Maui 9.1.3. I need to figure out why. It only affects iOS and MacCatalyst.
 *               Also oI removed some code files that were no longer used. mdail 10-2-24
 *  v 5.033.0422 Fixed the ask view, I had missed a couple of the setup things for it mdail 10-2-24
 *  v 5.033.0421 Changed the main page loading so it only load the content & anskview when OnSizeAllocated runs. Hopefully that should speed up
 *               the loading of the main page as it is not doing a bunch of things twice. mdail 10-2-24
 *  v 5.033.0420 Made some changes to try and speed up the loading of the data in the SelectFlashView & SelectPracticeView, I changed the
 *               removed some of the await run on main thread as they didn't seem to be needed as they were in the wrong place 
 *               anyway. I still need to test it, if it's still slow I nned to test it on the iPad to see if Its better there. mdail 10-1-24 
 *  v 5.033.0419 Fix the splash screen to show in the better colors with a light blue background mdail 10-1-24
 *  v 5.033.0418 Fix the save and restore to use JSON instead of XML, The save didn't work. I also changed some of the code that
 *               loads the  data to use parallel tasks to speed it up. mdail 10-1-24
 *  v 5.033.0417 Changed the save and restore to use JSON instead of XML, It took awhile to get the AI to properly update the check
 *               based on the saved file to true or false and add or remove it from the SelectedCategories collection. 
 *               NEEDs to be tested to make sure it works properly and is faster than it used to be mdail 9-30-24
 *  v 5.033.0416 Added OnAppearing to the SelectView & move SaveSelectedCats & SetSelected to the SelectViewModel and made it so
 *               that SetSelected is called in OnAppearing and SaveSelectedCats is run form OnDisappearing. to fix it for iOS & Mac
 *               as the selection was not being reload navigatiing back and I didn't want to run it twice in iOS & Mac. mdail 9-30-24
 *  v 5.033.0415 Fixed button on main page cutting off the bottom of the text when the ask view was visible on smaller screens.
 *               I also made the button a little bigger when the Ask view wasn't visible. Fixed the progress bar being too small
 *               on iPhones and iPads, might need to fix it for Mac also. Need to change the ScaleX and ScaleY to make it change.
 *               Need to continue testing on all iOS & Mac. mdail 9-26-24
 *  v 5.033.0414 FIxed windows scrolling questionin only one direction mdail 9-26-24
 *  v 5.033.0413 I turned off the animation for the CarouselView in the FlashcardView and it seems to have fixed the problem with the 
 *               random button going it preminate scrolling up thru the cards. It does still sometimes come up inbetween cards, but it
 *               does it less often and it is not as bad as it was before. mdail 9-26-24
 *  v 5.033.0412 Still trying to fix the random button from putting windows into contnuious scrolling up thru the cards, It seems
 *               that it might be related to the animation that is built in to the CarouselView, I need to try it with that set to
 *               false for all moves in windows, Also I need to find out what is happening on the first next when the cards load and 
 *               the first time next is pressed it does next, previous from there on out it works .mdail 9-25-24
 *  v 5.033.0411 Still trying to fix the random button from putting windows into contnuious scrolling up thru the cards mdail 9-24-24
 *  v 5.033.0410 trying to figure out how to fix the random buttonfrom putting windows into contnuious scrolling up thru the cards mdail 9-23-24
 *  v 5.033.0409 Fixed the flipped not being reset by setting flipped to false for the previous position in the CarouselView.PositionChanged
 *               for the flashcards to make sure the card is always question up mdail 9-23-24
 *  v 5.033.0408 Adjust vertical options and margin for the SelectFlashView & SelectPracticeView to make it look better on Pads mdail 9-20-24
 *  v 5.033.0407 Intermittently if I use MainThread.InvokeOnMainThreadAsync(async () => in the OnBackButtonPressed method it will crash the app
 *               when running in Windows so I removed the InvokeOnMainThreadAsync from the OnBackButtonPressed method all pages as I 
 *               don't think it needs it for that method mdail 9-20-24
 *  v 5.033.0406 Update some nugets to the latest version mdail 9-17-24
 *  v 5.033.0405 Fixed the position being off by when using the CarouselView.Position, Now most things just use the CurrentQuestion 
 *               and not a position to get the information from the question. Also fix trying to read the saved selections 
 *               if the file did not exist, It might have only been a problem on Windows, howewver it fix for all mdail 9-13-24
 *  v 5.033.0404 Fixed the logo size ot changing for different screen sizes by adding the binding to AppSozes.XAML. mdail 9-13-24
 *  v 5.033.0403 Moved the bound commands back to the base view from the base view model for the Flashcard, Practice & Quiz views mdail 9-13-24
 *  v 5.033.0402 Moved the bound Command from the base view to the base view model from the Flashcard, Practice & Quiz view mdail 9-13-24
 *  v 5.033.0401 Changed the check marks to use images of a box in blue and a checked box in white instead of the checkbox which might have
 *               fixed the crashes. I also noticed that the checkbox sizes were not being change for different screen sizes so I added a the
 *               binding of the size to the BaseHeightRequest in the XAML. mdail 9-13-24
 *  v 5.033.0400 Move the code for TrackSelectedCurrentItem to inside of OnSelectionChanged to make sure it wasn't causing a race until 
 *               SelectedItem = null was exicuted to see if that was causing the crash. It wasn't mdail 9-12-24 
 *  v 5.033.0399 Removed the Cat Index property from the Categories class as it wasn't used anyway and was just in the way. Removed the setting
 *               of the check box handler so it doesn't use the custom checkbox as it had problem and I don't think it wa the problem anyway. 
 *               Looking at the tombstone, changing color of the checkbox is part of the failure, as it was using a combo button when the
 *               custom checkbox was in use and a checkbox when it is not in use and you can see that in the dump. mdail 9-12-24
 *  v 5.033.0398 Still trying to fix the bug that is causing the app to crash in the SlectFlashView & SelectPracticeView when trying to set
 *               saved the selected categories/ Select All or Deselect All. I have change the Select & Deselect to update the SlectedCategory
 *               and not to use the OnSelectionChanged method to update it. I've added checks for nulls, I added makeing a new Category before
 *               trying to remove incase it is null. I made a custom handler for the Checkbox and it still having prolems. Note: the custom
 *               handler is only tested ion Android. mdail 9/1o/24
 *  v 5.033.0397 Still hunting for the bug that causes the app to crash when trying to save the selected categories in Flash &/or Practice mdail 9/5/24
 *  v 5.033.0396 Finished converting the SelectFlashView, SelectPracticeView to use the base class & ViewModel  SelectViewModel & SelectView mdaill 9/5/24
 *  v 5.033.0395 Started converting the SelectFlashView, SelectPracticeView to use the base class & ViewModel  SelectViewModel & SelectView
 *               still need ot see if I cna convert any more of the methods to use a base version for ease of codeing & test    mdail 9/4/24
 *  v 5.033.0394 Changed the name of the collcection view in the SelectFlashView and SelectPracticeView XAML and use a public CatCollection 
 *               to access it so that it is not the same XAML name in both views. I also change the way the colors are set in the Categories
 *               to make it easier and quicker. I also added a method to the Categories class to set the colors to the 
 *               selected/unslected colors. mdail 8/30/24
 *  v 5.033.0393 Still trying to fix the intermittent crashing of the app due to trying to save and preset the prior selections of the
 *               Flash & practice categories. I have added a try catch to the SetSelected method in the SelectFlashViewModel and SelectPracticeViewModel
 *               to see if it will catch the error and print it out. It is failing at category.ItIsChecked = !category.ItIsChecked, which makes
 *               absolutely no sence to me.mdail 8-29-24
 *  v 5.033.0392 Added code to Initialize the SelectedCheckTextColorLocal & SelectedBackgroundColorLocal in the Categories class so that they
 *               can't be null, also added code in the setter & getter SelectedCheckTextColor & SelectedBackgroundColor to eliminate nulls. Still
 *               trying to figure out where the null is comming from when trying to set the FlashcardCategories in the SelectFlashViewModel.
 *               and the PracticeCategories in the SelectPraticeViewModel & the views for each. Added more bedug statments to try and figure it
 *               out also. So far it doesn't make any sence as it is only printting 2 debug statements when it crashes. mdail 8-27-24
 *  v 5.033.0391 Added the method ScrollToCategoryAsync to the SelectPracticeViewModel and SelectFlashViewModel to await the scroll to the 
 *               selected category when setting the selected categories. I also added await to the SetSelected method in the 
 *               SelectPracticeViewModel and SelectFlashViewModel to await the PresetSelected. mdail 8-27-24
 *  v 5.033.0390 Finished converting the CardViewModel and CardView to use the BaseViewModel and BaseView classes. I had to add a make
 *               the SetView a virtual method so I could override it in the QuizViewModel as it set the Cards to either the Review or Question.
 *               I also found that the View Flagged only in the menu was set to get the Hidden question and fixed that. mdail 8-26-24
 *  v 5.033.0389 For easy of maintainace I made a Base ViewModel class CardViewModel that has the OnPropertyChanged method and 
 *               the MainThread.InvokeOnMainThreadAsync and a BaseView class CardView because alot of the code was the same for the
 *               FlashcardViewModel, PracticeCardViewModel and QuizViewModel and the FlashcardView, PracticeCardView and QuizView. So far I
 *               have the Flashcard working using the BaseViewModel and BaseView, I need to do the same for the Practice and 
 *               Quiz views mdail 8-23-24
 *  v 4.032.0388 Had to add a GestureRecognizers to the Grid the fills the whole screen in the ReviewQuestion, QuestionAlert, AlertPage
 *               and InformationAlert with a Dummy{PageUnique}Command that has a new Command(() => { Does nothing })  to catch the 
 *               tap event and stop it from going through to the views behind them. mdail 8-22-24
 *  v 4.032.0387 Try setting the InputTransparent="False" for the ContectView in the ReviewQuestion, QuestionAlert, AlertPage
 *               and InformationAlert to see if that will keep the views behind them from being able to be clicked on. mdail 8-22-24
 *  v 4.032.0386 Changed the ReviewQuizPopup to the ReviewQuizQuestion which is using a ContentView on the Quiz pagfe and not the 
 *               popup from the Community toolkit. I had to adjust the view to get it to display the way I wanted it to mdail 8-22-24
 *  v 4.032.0385 Fixed it so all back button call MainThread.InvokeOnMainThreadAsync before calling the back button code, this might
 *               fix the Expected: main Calling: Thread-3 mdail 8-22-24
 *  v 4.032.0384 For some reason when the screen rotates from landscape to portrait the CarouselView move forward one card, so to 
 *               keep the same card in view I had to subtract 1 from the position when the screen is rotated before scroll to it after
 *               rotation which is required because if not the cards are not centered properly after rotation. mdail 8-22-24
 *  v 4.032.0383 Set the CarouselView to not loop, and to use PositionChanged to let the use know when the try to swipe past the 
 *               first or last card mdail 8-22-24
 *  v 4.032.0382 Somehow the flashcard on swipe back if it goes from the first to last card it reset to first, I need to figure out why
 *               as make it so the Practice & Quiz views do the same thing. mdail 8-21-24
 *  v 4.032.0381 Convert back to using CarouselView for the FlashcardView, PracticeCardView and QuizView. I had to add a grid around the
 *               CarouselView to get it to resize when rotated & clear the RowDefinitions & ColumnDefinitions when the screen is rotated to
 *               resize it properly. I stopped usiing the CurrentIndex in favor of the Position property of the CarouselView. When ever there is
 *               move than one place the position is used I make a int position variable and set it to the CarouselView.Position. so I don't 
 *               need to read it more than once. I update the progress when the CurrentQuestion is set. I also added a RefreshCommand to the to
 *               be able to set the Grid RowDefinitions & ColumnDefinitions when the screen is rotated mdail 8-21-24
 *  v 4.031.0380 Completed the CustomScrollViewHandler so we can handle the vertical scrolling in the CustomScrollView and pass 
 *               everything else through. It needs to be tested on all platforms. Also added an updated version of the CustomCardView which
 *               will hopefully handle the scrolling better. mdail 8-17-24
 *  v 4.031.0379 I have the CustomCardView working for swiping & scrolling in the Flashcard View in Android & Windows. I had to fix the 
 *               Toast message to work for windows be showing it streight from the CustomCardView class. mdail 8-16-24
 *  v 4.031.0378 I was able to get the page change animation to work by using the for the CustomCardView so now it is working as expected.
 *               I still have a problem with the layout only using the top part of the screen mdail 8-14-24
 *  v 4.031.0377 Started writing my own code to handle the card view scroll right and left, So far I have the dispaly showing and
 *               it will scroll with the button, however I am not getting animation. When swipping the card is blank however the animation
 *               is working. I need to figure out why the card is blank when swipping and why animation not working on button change. The Random
 *               however is working even with animation. It might be I need to make sure the methods are async. Also the Border around the 
 *               flashcard isn't filling the screen right now. I may need to figure out how to do a pass througCarouselViewh of the swip up & down
 *               for horizontal scrolling of the question. mdail 8-13-24
 *  v 4.030.0376 Changed it so the InforamtionAlert message always starts with a Captial letter. mdail 8-12-24
 *  v 4.030.0375 Changed Grid Sizing for the AlertPage to use Auto for the first row and * for the second row to make sure the second line of 
 *               the alert message is displayed, Made the same change to the InformationAlert & QuestionAlert mdail 8-12-24
 *  v 4.030.0374 Someting is not adding up, the The hidden questions have been cleared! isn't getting the second line displaied in the AlertPage
 *               I just tried adding a Constants.NEWLINE to see if that fixes it. Howeve I need to figure out how to really fix it as the 
 *               InformationAlert could have the same issue, and I need it to work. mdail 8-9-24
 *  v 4.030.0373 Set the grid row 2 to * instead of Auto because it sometimes did not grow for the whole line in the AlertPage mdail 8-9-24
 *  v 4.030.0372 Resized the image down to 32 pixels to try and fix the image showing up too large in iOS release mode mdail 8-9-24
 *  v 4.030.0371 Resized the original navy home image to 80 pixels to try and fix it showing up too large in iOS release mode mdail 8-9-24
 *  v 4.030.0370 Added an Aspect="AspectFit" to the image that holds the home image mdail 8-9-24
 *  v 4.030.0369 Put the Flyout menu header inside a grid so I could limit the size of the Image to be 70% the width of the Flyoutmenu mdail 8-8-24
 *  v 4.030.0368 Fixed the profile for the MacCatalyst & iOS so it would build and run on the MacCatalyst & IOSin release mode mdail 8-8-24
 *  v 4.030.0367 Fixed the XAML Compilation for the SmallTabletSizes file by really getting it to disable mdail 8-8-24
 *  v 4.030.0368 Updated 3 Nugets, cleared obj & bin, restored Nugets, logged into Apple Developers Account and accepted new dev agreement as
 *               it had locked me out of my account and signing profiles, Disable XAML Compilation for the SmallTabletSizes file mdail 8-8-24
 *  v 4.030.0367 Changed the SHow & Hide of the progressbar to async, added MainThread.InvokeOnMainThreadAsync, await's,  ForceLayout and 
 *               await myContentView.FadeTo(1, 250) for a Fade in animation to get the progress bar to always show up in iOS as it was only showing 
 *               up sometimes. Fixed some of the Back buttons being commented out still from trying to use the FlyoutItem for faster navigation.
 *               Updated the SQL nugget and tested on iOS * Mac including the iPad Mini which is 8.5 inches so it still gets the Larger fonts which
 *               still looked fine mdail 8-7-24
 *  v 4.030.0366 There is a strainge burg that causes the app to crash in debug mode on Windows, as it doesn't cause the problem in release mode
 *               there doesn't seem to be a reason to fix it, so I can't go to review quizzes first the back to main page in debug mode.
 *               Also I can't use the faster navigation of the flyitems as it causes the back button to not show up. mdail 8-6-24
 *  v 4.030.0365 Fixed Back button to use MainThread.InvokeOnMainThreadAsync instead of the other as the AI was wrong the first time mdail 8-6-24 
 *  v 4.030.0364 Changed all back buttons to use  Dispatcher.Dispatch(async () => and OnBackButtonPressed overides to use
 *               await Shell.Current.Dispatcher.DispatchAsync(async to fix intemittent InteropServices.COMException error on WIndows mdail 8-6-24
 *  v 4.030.0363 Changed the Menu to use DisplayAlert on Windows instead of the AlertPage. The AlertPage works on other platforms 
 *               but causes main page to not accept input Windows. Changed the Review Quizes page to load the data when it loads and
 *               fixed the loading of the data so it clear the data if there are no records to load. mdail 8-6-24
 *  v 4.030.0362 Forgot to remove something from the AlertPage that was added trouble shooting the windows [problem mdail 8-5-24
 *  v 4.030.0361 Split the clear all reviews in two methods both asnc so they should wait until the first is done before starting the second
 *               runs. I am having trouble with the AlertPage when running on Windows, I work in Android however when I run it on Windows it
 *               does not all the MainPag to accept input after it is closed. I need to figure out why it is doing that and fix it. mdail 8-5-24
 *  v 4.030.0360 Fixed the Back button command, it wasn't actually checking the answer to the question, also the assignment of the 
 *               XAML views was missing for some reason. mdail 8-5-24
 *  v 4.030.0359 Finally go the Shell Back button so I could override the back button and use the defalut Back button and intecept it mdail 8-5-24
 *  v 4.030.0358 Changed it so it will use the Shell navigation and back button for Windows & Android, for iOS and MacCatalyst 
 *               Shell back button will be hidden and a new Back Button added to the toolbar so I can control the back button mdail 8-5-24
 *  v 4.029.0357 Changed the Back button addition to the toolbar to be in XAML instead of code behind and fix resigistering 
 *               SlecetPractice incorrectly mdail 8-2-24
 *  v 4.029.0356 Changed it so the Select Flash, Practice & Quiz and the Review Quiz views all have a back button that says back and 
 *               are using the Shell Navigation. Those same view are now a FlyoutItem instead of MenuItem. I still need to figure out 
 *               how to move the back button on WIndwos & Android to the right of the screen. mdail 8-2-24
 *  v 4.029.0355 Got the AlertPage to work as expected, I had to change the way the TaskCompletionSource was set up and used. I also got
 *               the progress bar to show from the FlyoutMenu on Clicks that take enough time to need it like the Flashcards, etc. I fix the 
 *               Information & Question Alert as it was set with the grid to be not visible. mdail 8-2-24
 *  v 4.029.0354 Fixed the QuestionAlert to reset the task after waiting for the user to answer the question. so it works the second time
 *               the question is asked. Still needs to be tested to make sure it work as expected. Also need to check for the app crashing
 *               (the time it di I went to Flash, practice, the Quiz then it crashed I think) mdail 8-1-24
 *  v 4.029.0353 Fixed the AppShell logic that check to see if the current page was the MainPage if so show the Menu Icon and if it was not
 *               it would not show the back button mdail 8-1-24
 *  v 4.029.0352 Removed the background color from the grid InformationAlert & QuestionAlert as it was making the whole background black mdail 8-1-24
 *  v 4.029.0351 Fixed the InformationAlert & QuestionAlert to have the background the only part that the Opacity="0.5" is applyed to mdail 8-1-24
 *  v 4.029.0350 Fixed the ProgressBarOverlay, InformationAlert & QuestionAlert to work show and hide whole view instead of just the grid as
 *               it was leaving the greyed out background when it was hidden. mdail 8-1-24
 *  v 4.029.0349 Added AlertPage to display infromation like an alert dialog. THis way I can use it with the Shell top display it over other
 *               pages, then remove it when the user clickes Exit. mdail 8-1-24
 *  v 4.029.0348 Changed Send Feedback Emails & the AppShell to show DisplayAlert's instead of my messages as they no longer work in those
 *               situations because of using Shell mdail 5-8-24
 *  v 4.029.0347 Chabged the QuestionAlert and InformationAlert to use the be Views the show or hide like the Progress Bar Overlay instead of 
 *               pages because the pages were not tranlucent and completely coverd the page and also the question alert never returned the answer
 *               so it didn't work properly.mdail 7-31-24
 *  v 4.029.0346 It seems that using shell I will have to change the Question & Information Alerts to act like the Progress Bar Overlay becasuse
 *               now it is ot working as it should, the question never returns the answer and it's background isn't transparent. I havn't checked 
 *               the Information Alert yet. I need to see if it works on Android now and doesn't crash when going back and forth between the 
 *               flashcard and main page then practice and main page then to flashcard again as it seems to work on windows now. I had to 
 *               remove the Progress Bar Overlay from the flyout menu as it was causing the app to crash on the windows trying to 
 *               start the app mdail 7-30-24 
 *  v 4.029.0345 I think I finally got the Shell to work the way I want it to work. The Back button seems to work properly now, I need to make
 *               sure it alows me to stop the exit on the Quiz. I need to make sure the information & Question alert work, I also need to figure 
 *               out why the progress bar isn't working from the menu. I need to make sure the IsREVIEW gets set & reset roperly and test all 
 *               funtions of the app to make sure it works mdail 7-26-24
 *  v 4.029.0344 Still trying to get the Shell and the progressbar to work properly and the way I want it to work, I have to add the progress 
 *               bar to the other pages that have loading screens. I am also trying to ge the Back Button figured out so it work properly so far
 *               I can't get the page to navigate backward properly mdail 7-26-24
 *  v 4.029.0343 Added a progress bar to the main page so I could stop using the Loading screen, I need to add the progress bar to the
 *               other pages that have loading screens and then test it on all platforms mdail 7-26-24
 *  v 4.029.0342 Converted the App to use Shell for navigation, I had to change the way the back button was handled in the Quiz page
 *               and move the flyout menu to the Shell, it will be disabled on all other pages, added back button to all other pages
 *               I need to test it on all platforms and make sure it works mdail 7-26-24
 *  v 4.028.0341 The CustomPage acts like it should work yet it doesn't, I made a new Flyout menu and it works except I can't
 *               get the measurements of the layout to work, it alswys return -1 so it never set properly, the old FlyoutPopup
 *               however by adding the measurements to OnAppering fix it not working in landscape on iOS. The screen sizes work for
 *               iPad & iPad mini (they use the larger sizes just fine). However I did have to add two meausrement for the flyout menu mdail 7-25-24
 *  v 4.028.0340 Fix the CustomPage Hanler so the Quiz page accepts the XAML portions that it did not before mdail 7-25-24
 *  v 4.028.0339 Try to use the CustomPage handler and see if maoving the if ios & mac helped fix the issues, it doesn't mdail 7-25-24
 *  v 4.028.0338 Went down  path trying to use a custom page handlers however I ran into problem a reversed most of it out mdail 7-24-24
 *  v 4.028.0337 If in the select quiz page I remove the loading page before I load the Quiz page The when I try to change the 
 *               back button, I can see thjat I an working on the loading page, if I remove the loading page first I can not
 *               get it to truely change the back buton. The is one option that causes it to not load the quiz page at all mdail 7-23-24
 *  v 4.028.0336 I hope this time I found the real answer to replacing the back button in iOS and macOS mdail 7-23-24
 *  v 4.028.0335 Try using a custom render and intercafe to replace the back button on the quiz page for iOS and macOS mdail 7-23-24
 *  v 4.028.0334 Changed the CustomBackButton to use an action and set it inside the QuizView instead of trying to set it in the 
 *               CustomBackButton class, fixed the back_white image. it now is using the chevron_left_white image from the shared resources
 *               folder mdail 7-23-24
 *  v 4.028.0333 Changed the way the app gets a reference to the UIViewController in the CustomBackButton class by passing in a reference
 *                to the ContentPage and then using it's handler to reference it as a UIViewController mdail 8-10-24
 *  v 4.028.0332 Gave up trying to us DependencyService and just used a class with compiler directives to handle the 
 *               custom back button mdail 7-23-24
 *  v 4.028.0331 Reconfigured the DependencyService to use the new Maui way of registering the service in the Program.cs file
 *               and figured out the name space and why it was complaining when I tried to register it mdail 7-22-24
 *  v 4.028.0330 Fix ICustomBackButton when it called SetLeftBarButtonItems it needed to call SetLeftBarButtonItem mdail 7-19-24
 *  v 4.028.0329 Fixed the project name of the MacCatalyst project so it would build and run on the MacCatalyst mdail 7-19-24
 *  v 4.028.0328 Added the missing registration for the ICustomBackButton interface in the App.xaml.cs file mdail 7-19-24
 *  v 4.028.0327 Added an interface to add a custom back button to iOS and MacCatalyst in the Quiz page so I can handle the back button 
 *               being pressed and ask the user if they are sure they want to exit the quiz page. mdail 7-19-24
 *  v 4.028.0326 Tried to fixed the back button on the SelectQuizView, PracticecardView, QuizView and SelectCatFlashView 
 *               to work on macOS & iOS only, they will only work if the back button in the Navigaiton bar is pressed. Fixed 
 *               SelectCatFlashView so the current selections will get saved when the page is disapearing no matter what. mdail 7-18-24
 *  v 4.028.0325 implented a custom back button on all pages so that it will work in macOS and iOS, it simply calls the 
 *               OnBackButtonPressed mdail 7-18-24
 *  v 4.028.0324 Removed the custom handler for the back button navigation page mdail 7-18-24
 *  v 4.028.0323 Try to add custom handler for back button navigation page mdail 7-18-24
 *  v 4.028.0322 Tried to change it so the Toast message would show in the middle of the bottom of the screen, however it didn't work because 
 *               the message would have to be inside the collection view and that make it not assesable in the code behind. I put it back mdail 7-16-24
 *  v 4.028.0321 Changed it so if running on windows the first & last questioin messaege is displayed in the middle of the bottom screen as 
 *               the toast message wasn't showing or would show as a notification instead of a toast message. mdail 7-16-24
 *  v 4.028.0320 Finally got the Sizes & SmallTableSizes to work for the different devices, I had to remove then From the Resources folder
 *               add a name to the XAML file and add an initalizer to them then add them to the App.xaml.cs file to load the correct one. mdail 7-15-24
 *  v 4.028.0319 After setting the sizes with a simple sizes.xml I need to add other files so that I could change the sizes for large
 *               and small tablets, I added a SmallTabelSizes.xml for the small tablets and the sizes.xml sets the sizes for evertything
 *               else. A method in the App.xaml.cs file get the screen sizes then reads the correct file and sets the sizes for the app.mdail 7-12-24
 *  v 4.028.0318 Start fixing so it changes sizes of images and fonts for different size devices, removed the Rectangle from the 
 *               InformationAlert & QuestionAlert and changed the background color of the Title label to blue and FillAndExpand for tne
 *               Horizontal options. The Sizes xmal works well for the settings in windows. I am not sure what to do for small Android
 *               Tabelts as they will likly be the problem. mdail 7-5-24
 *  v 4.028.0317 Had to update nugets to make it work for MacCatalyst in release mode. mdail 7-5-24
 *  v 4.028.0316 Fixed the ReviewQuiz not showing total time at the top, Changed the Score Quiz and Exit Quiz to use the QuestionAlert
 *               instead of using a DisplayAlert. Also fixed a bug if you scored and choose to review the quiz if you then went back to the
 *               to the Select Quiz and selected a Quiz it would put the quiz into review mode. mdail 7-2-24
 *  v 4.028.0315 Fixed the Ask Portrait view crashing windows when it tried to load the view, I changed the size of the Exit X Glyph
 *               button, due to it causing problems on windows it is still larger than I wanted in Android mdail 6-28-24
 *  v 4.028.0314 Set Max & Min height request for the information alert, because it messes up the sizes if I try to change thing on 
 *               rotation if the message label is multiline I am not changeing the sizes for landscape or anything else. mdail 6-28-24
 *  v 4.028.0313 Changed the Custom Alert Dialogs to use bindings for the title and message, also changed the way the QuestionAlert
 *               as it was not waiting for the responce from the user before finishing the OnTap_Practice method which always made the 
 *               responce false. mdail 6-29-24
 *  v 4.028.0312 Changed some methods that should not have been async, made the UpdateProgress methods run the MainThread themselves 
 *               instead of having the calling methods do it. Fixed the QuestionsAlert to make sure it waited for the answer. mdail 6-28-24
 *  v 4.028.0311 Removed the Lock_It button from the Practice view, as it was was still using space even though it was not visible.
 *               Also clean some unused lines of code and variables. Also add code to change the Quiz title when it is in review mode
 *               to Review Quiz Quiestions mdail 6-28-24
 *  v 4.028.0310 I finally actually got it fixed by removing a rectangle that I put behind the label with the message in it to make
 *               sure the background color was white, the title label has a rectangle behind it to make sure the background color is bule,
 *               however that one doesn't seem to cause the Alert to grow on rotation. mdail 6-27-24
 *  v 4.028.0309 Make the AInformationAlert look better, Fix for Explatations that need Carriage Returns. Added QuestionAlert to ask if
 *               the user is sure this is there final answer. mdail 6-27-24
 *  v 4.028.0308 Changed it so the Practice uses a custom alert, trying to fix the Information alert to look better, need to test it on
 *               all platforms especially in landscape mdail 6-26-24
 *  v 4.028.0307 Removed the instruction label from the Quiz so it matches the Practice view mdail 6-26-24
 *  v 4.028.0306 Changed the practrice mode so it doesn't use the check make or lock buttons, instead it asks if the selected answer is the users
 *               final answer, if it is it locks the question and if it isn't it doesn't lock the question. mdail 6-25-24
 *  v 4.027.0305 Fix the Quiz grid layout width not recalculating when the screen was rotated mdail 6-21-24
 *  v 4.027.0304 Removed a line of code that was for testing only,  Tested in tablet font and Logo code for iOS too mdail 6-20-24
 *  v 4.027.0303 Added code to change the landcape logo back to the original logo tablets in landcape mode, and change the size 
 *               of the logo based on whether it is a small or larger tablet and whether the AskView is visible or not. mdail 6-20-14 
 *  v 4.027.0302 Added code to change the font sizes of the buttons and lablel on the main screen for a Tablet mdail 6-20-24
 *  v 4.027.0301 Fixed the Flyout menu so it calculates exactly how tall the menu is and then sets the height of Row2 row Absolute 
 *               if the menu is taller than the screen so the menu will scroll. Added some margin spacing to the cards. mdail 6-18-24
 *  v 4.027.0300 I found that the Flip animation was causing the app to crash on the Winodws, so I now have it fadein-fadeout flip for windows. The
 *               move to random now doesn't use the CollectionView animaion it shrinks to grows (one Desktops it goes to 0.5 and Phones to 0.8).
 *               I still need to test these changes on the Mac and iOS. The Flip has been changed from the orginal to a better flip mdail 6-13-24
 *  v 4.027.0299 Added a right margin for the labels in the Practice & Quiz views to make sure the text doesn't go touch the edge of the screen.
 *               Also updated the version number in the project file soi it will display on the screen. mdail 6-13-24
 *  v 4.027.0298 Fixed the Practice & Quiz (Also changed the code in Flashcard) to insure the size of the card filled the screen by changing
 *               the WidthRequest reference to use the top level stack layout width instead of useing the CollectionView width as for some reason
 *               when I was trying to add space around the letters at the top it changed the width and stopped filling the screen. Added code to 
 *               skip compiling the xaml for the select cat flash view because fo some reason it started complaining the it couldn't update the
 *               dll file due to the dll being used by some other process. Bumped the SupportedOSPlatformVersion down to 13.1, as for some reason 
 *               it had gotten set to 14 which is the latest release of macOS only. And updated 2 Nugets mdail 6-12-24
 *  v 4.027.0297 Update some of the Nuget packages. Fixed AnswerANBCompare to handle the answers with "; See Explaination" also. mdail 6-10-24
 *  v 4.027.0296 Fix AnswerANBComparer to handle the All of these; See Explanation. If the OnBackButtonPressed so it exist if the question is 
 *               answered yes and doesn't if it is answered no if the Constants.REVIEW is false it also does Navigation.RemovePage(this) 
 *               and return true and works in windows and android mdail 6-7-24
 *  v 4.027.0295 Fix app crashing trying to display the quiz in release mode due to the width reqest being wrong mdail 6-7-24
 *  v 4.027.0294 Changed the Font sizes for the Cards and select list labels. Added the RefreshView to all of the cards, changed the practice
 *               & quiz scrollview horizontal options to FillAndExpand and the vertical options to FillAndExpand.
 *  v 4.027.0293 tried to add code to hanle the back button in iOS, MacCatalyst and Windows in the quiz and found the only way would
 *               be to convert the whole app to use the Shell navigation style mdail 6-5-24
 *  v 4.027.0292 Added a Behaviour to the CollectionView in the FlashcardView to handle the reseting the Flipped when the card was 
 *               swiped out of view. Fixed the Practice card StackLayout not having option set correctly so the card didn't always load
 *               all the answer, then when the card started to scroll it would fill in the rest of the answers. Fixed two questions that had 
 *               an answer 4 but no answer 3 and were True/False anyway. Fixed it so the back button on at least most of the pages says back
 *               as it seems if you set the title property of the page in code behind it sets the back button to back. I still need to make sure it
 *               does that on all pages and also does it in ANdroid, Windows and MacCatalyst. Set the Title of the Main page to the proper name. And
 *               updated the database version. mdail 6-1-24
 *  v 4.027.0291 Update to the latest version of AppCenter mdail 5-31-24
 *  v 4.027.0290 Finally have the CollectionView working in the FlashcardView, PracticeView and QuizView. I had to handle the swipe gesture
 *               right & left myself as the CollectionView doesn't have a built in way to do it. I also had to handle the selected question
 *               and index changing myself also. I had to put the ScrollView back into the Practice & Quiz for the long answewr also. It
 *               appear to be working now I need to test it further and on iOS, Mac & Windows mdail 5-24-24
 *  v 4.027.0289 Fix duplicate binding in SelectCatFlashView and ReviewQuizzesView and fix binding errors in ReviewQuizzesView xaml file.
 *               Also added CurrentQuestion to both Practice & Quiz view models mdail 5-21-24
 *  v 4.027.0288 Remove duplicate binding from Practice & Quiz views, rename CurrentPosition to CurrentIndex in the Practice & Quiz view models
 *               to prepare to convert to CollectionView. Now all binding is done in xaml and not repeated in code for the Practice & Quiz views. mdail 5-21-24
 *  v 4.027.0287 Got the CollectionView to work in the FlashcardView exceptably well. Now I need to update the Practice and Quiz views to 
 *               use the CollectionView and make sure that they work as well, I also learned a new way to get the reference to the ViewModel
 *               in the view, and as I already have the biinding set in the view I don't need set the binding context to the View. So it doesn't
 *               have to run the code twice when loading the page. mdail 5-16-24
 *  v 4.026.0286 Started trying to convert the FlashcardView to use the CollectionView instead of CarouselView, having trould getting the 
 *               position and progressbar to update, also still having problem getting the flashcard to always snap to the correct position
 *               when the user is scrolling and stops before completing the scroll. mdail 5-15-24
 *  v 4.026.0285 Try to fix screen rotation by resetting ItemSource for the CarouselView in the FlashcardView.xaml.cs Mdail 5-14-24
 *  v 4.026.0284 changed the setup methods to not us postion input, removed the the ScrollTo methods and just change position by assigning 
 *               the value to the CurrentPosition property in Flash, Practice and quiz modes (the random was still using, however it doesn't 
 *               anymore. It seemd to lose the animation after awhile even when I added it back in after assigning the postion. Sill need 
 *               more testing. mdail 5-14-24
 *  v 4.026.0283 I think I fixed the problem with the progress bar not updating properly, I set 2 way binding on the position to CurrentPosion
 *               and then don't use scroll to just update the position except for page oriantation changes. mdail 5-10-24
 *  v 4.026.0282 Fixed Practice & Quiz loading problems in iOS. FIxed most of the problems with the position not updating properly. FIxed
 *               the elapsed time to displaying. Still have problem after using the next button a few time then scrolling with a swipe, sometimes
 *               the progress doesn't update and then the question's answers can't be selected, untill then previous and then next button are 
 *               tapped mdail 5-10-24
 *  v 4.026.0281 On the iOS simulator scrolling the page makes it lose track of where it is in the progress bar, it work on the iPad though
 *               so I don't know if it's just a simulator thing or not.mdail 5-10-24
 *  v 4.026.0280 Fixed the Practice & Quiz views to paying any attention to the left margin of the screen, I hadd to remove the scroll view
 *               that the border was wrapped in. mdail 5-10-24
 *  v 4.026.0279 FIx the button at the bottm prev, next & score overlapping each other and the work previous not showing all the 
 *               text sometimes as it was covered by the score button. mdail 5-10-24  
 *  v 4.026.0278 Fixed FontAwsome not working in Android by replacing it with an close icon image mdail 5-9-24
 *  v 4.026.0277 Updated Nugets and fixed it crashing in Windows, Updated copyright date, Changed the way I was using FontAswome
 *               which worjs in windows but not Android, fix color when splash screen is supossed to show in Android mdail 5-8-24
 *  v 4.026.0276 Added reload current item to pracitce & quiz view models to make sure the current item is reloaded when the page is 
 *               reloaded mdail 5-8-24
 *  v 4.026.0275 Finally got a cert-profile set that works for loading the app to the iPad again mdail 5-8-23
 *  v 4.026.0274 Added platrom specific entries to set up for sending emails for feedback, however it doesn't seem to work
 *               on the macOS. mdail 5-3-23 
 *  v 4.026.0273 Fix selected items getting lost if the tap was on the checkbox, the chckbox would handle the tap and 
 *               bypass the CollectionView_SelectionChanged. I had to set the InputTransparent="True" on the checkbox so it gets handled
 *               the sameway as a tap anywhere else on the item mdail 4-30-24
 *  v 4.026.0272 Removed the background color from the selection button in the Quiz and Practice views mdail 4-30-24
 *  v 4.026.0271 Fixed the DBVersion, it would sucessfully copy the database if the version was different, however it would then fail 
 *               to read the new version because I had forgotten reset the db variable to a new SQLiteConnection. I also added a try catch
 *               and a retry to the Delete file if it failed to delete the file the first time. I also check if the system is win or mac 
 *               and reset the margins for the information popup to make it look better. Aslo fixed the flyout menu popup so oif it's in 
 *               landscape and not on win or mac the menu will scroll prperly. mdail 4-30-24
 *  v 4.026.0270 Fixed InformationPopuPage crashing the app when not in debug mode. Which some how fixed the Flashcard not loading when
 *               not in debug mode mdail 4-30-24 
 *  v 4.026.0269 Found and fix an issue in the FlashcardView.Xaml.cs which should have been what was stoppiing the cards from loading however
 *               it didn't fix the issue. It has worked for so much of the time it is making me crazy. I need to figure out what is causing the
 *               flashcard not to load when not run from the debug button. mdail 4-29-24
 *  v 4.026.0268 Still had one reference to self in ReviewQuizzezView.XAML.cs that needed to be changed to ReviewQuizzesViewPage mdail 4-29-24
 *  v 4.026.0267 Added Flashcard fix to the OnCurrentItemChanged method to make sure the card is correct for the first card, also
 *               add a null check for e.PreviousItem to see if that is what is causing the flashcard to not open mdail 4-29-24
 *  v 4.026.0266 Fixed the Quiz view in review mode not showing the answers due to not setting the Lock property of the question, also fixed the
 *               quiz view not setting the first question as it wasn't reloading. Fixed the Score Quiz button not being disabled when in review mode. Fix
 *               the Flashcard displaying None of these if it was an answer with All of these and one the other answers was None of these. mdail 4-29-24
 *  v 4.026.0265 Fixed the review quiz view, changed the reference binding from self to ReviewQuizzesViewPage which is the name of the page mdail 4-22-24
 *  v 4.026.0264 Added Mark G to the recipents list for the feedback emails and Changed the TrackSelectedCurrentItem method to try and fix intemittently
 *               having the app say there are no selected categories when there are mdail 4-22-24
 *  v 4.026.0263 Fixed practice so if the lock is pressed without an answer being selected it doesn't lock the question mdail 4-22-24
 *  v 4.026.0262 Fix bug that caused the database version to be set to 0 if the database didn't exist and had to be copied before the data was loaded mdail 4-22-24  
 *  v 4.026.0261 Changed the Citation, Explaination and Feedback buttons from using the clicked to using the pressed event as the clicked egnored the
 *               long press, so if the button clicking took too long it didn't happen makeing the button seem intermittent mdail 4-22-24
 *  v 4.026.0260 Did the fix to the Quiz view that I did to the Practice view, There are some differences that need to be tested for the review mode
 *               of the quiz view. Still need to teest the basic view in Android. Need to then test it all in iOS and MacCatalyst mdail 4-19-24
 *  v 4.026.0259 The fix for windows practice cards didn't work properly in Android so I had to put the images into grids and add the tap gesture
 *               to the grid instead of the image which seems to have fixed the problem. I also had to add a check for the lock being set on the 
 *               method inside the tap gesture recognizer to stop the user from selecting a different answer when the lock is set as I can't disalbe the 
 *               images as I could with the buttons mdail 4-19-24
 *  v 4.026.0258 Changed the PracticecardView to move the top select buttons to the top of the CarouselView, also changed the ImageButtons to
 *               Images using the same tap gesture recognizer as the labels. This seems to have fixed the carousel view going into endless 
 *               scrolling and locking the app up. I need to fix the Quiz to use the same layout and then test it to make sure it works mdail 4-19-24
 *  v 4.026.0257 Verified that call to SQLitePCL.Batteries_V2.Init() was not required for windows and removed it mdail 4-18-24
 *  v 4.026.0256 Minor update to the way the progress bar is updated, still having troulbes with windows scrolling mdail 4-12-24
 *  v 4.026.0255 Had to add check for lock being set on the qusetion in the quiz & practice view to stop the user from selecting a 
 *               different answer when lock is set mdail 4-5-24
 *  v 4.026.0254 Tried to make sure all UI operation use Dispatcher or await methods to run on the main thread mdail 4-5-24
 *  v 4.026.0253 Changed it so that if a position is needed more than once in a card view model that it is set to a variable and used,
 *               Also changed it so when code is run due to position changed it is forced onthe main thread mdail 4-5-24
 *  v 4.026.0252 Enlarged the previous & next buttons on the flashcard, reduce it them little in the quiz, Set the Random bytton to be
 *               disabled in windows as it causes all sorts of problems, I need to figure out how to submit them as a bug so 
 *               microsoft can fix it mdail 4-4-23
 *  v 4.026.0251 Set it so the MainPage uses portrait layout is used for WinUI and MacCatalyst, Landscape is used only 
 *               for Android and iOS. Even though the Portrait layout is used for WinUI and MacCatalyst it looks better mdaill 4-3-24
 *  v 4.026.0250 Finally figured out what caused it to fail on windows, it was the update to 8.0.14 of the
 *               Maui compatablity package, I had to change it back to 8.0.7 and it worked again mdail 4-2-24
 *  v 4.026.0249 Had to check for MacCatalyst and not start app center as it caused the app to crash, once
 *               I did that the app works on MacCatalyst, note I need to test it more I just tested 
 *               the start and menu screebs display so far mdail 4/2/24
 *  v 4.026.0248 Fnally got the flayout popup to act like a flyoutment as I wanted it to mdail 4-1-24
 *  v 4.026.0247 Fix error caused by renaming the MainDetailPage to MainPage mdail 3-29-24 
 *  v 4.026.0246 Changed the name of MainDetailPage to MainPage which is now causing in iOS error mdial 3-29-24 
 *  v 4.026.0245 !!!Note!!! when it says it can't find the mipmap icon for Android change the <MauiIcon swap TinitColor or BackgroundColor in the
 *                <MauiIcon Include="Resources\AppIcon\appicon.svg" ForegroundFile="Resources\AppIcon\appiconfg.svg" BackgroundColor="#FFFFFF" line in
 *                the project file. 
 *  v 4.026.0244 Changed the lock picture to a check mark for the practice to check the answer, turned of the animation for the infomation
 *               Popup so it just appears in th middle of the page (at least on Android), added a grey color behind the letters at the top
 *               to make them standout better and fixed the quiz not spacing the answers correctly and only showing the first line of each
 *               question mdail 3-29-24
 *  v 4.026.0243 finally fix getting an error message about mipmap\app_icon.png not found by changing the color to TintColor mdail 3-26-24
 *  v 4.026.0242 Fixed database check version not working. Fixed some crashes because it was running Task and await
 *               while update the screen. I had to make it run thing on the main thread mdail 3-26-24 
 *  v 4.026.0241 Renamed the solution, project namespace and the app name because I had the c and a swap mdail 3-26-24
 *  v 3.025.0240 Added a ReloadCurrentItem to the flash card view model and change the SetUp to take a int input to
 *               reload try to fix the explanation and citation on random clicks no always being correct, also
 *               the answer was showing the question again mdail 3-25-24
 *  v 3.025.0239 Stop using CurrentIndex and use the Position of the CarouselView instead, removed all the DataTypes
 *               from the xaml files as they cause problem with binding many places, and fix binding error that
 *               the datatypes caused me to make edits for mdail 3-25-24
 *  v 3.025.0238 Set all the datatype which made me change and fix all the bindings and binding reference so
 *               it would all compile again, now it just need to be tested, maybe it fixed the information 
 *               page?? too I am not sure mdail 3-22-24
 *  v 3.024.0237 Fix appearance of the information popup so it looks the way I want it finally, however it is
 *               still crashing when run without being in the debug mode mdail 3-22-24
 *  v 3.024.0236 Now I just need to fix the information popup crashing the app if it's not in debug mode mdail 3-21-24
 *  v 3.024.0235 Fixed the flip animation cause the app to crash, fixed the information popup page going
 *               off the edge of the screen when it had long text, improved the amination so it doesn't show
 *               the mis-sized popup and simply the correctly sized popup
 *  v 3.024.0236 Fix layout in flashcard for iOS as the old layout did not work, need to test other functions
 *               see if flip is fixed mdail 3-21-24
 *  v 3.024.0235 Added ms Crashlytics through the app center for all 4 variations of the app mdail 3-12-24 
 *  v 3.023.0234 Changed the Information Popup to use a Content page with a grid on it and a custom animation to show 
 *               the popup. It doesn't use the Community Toolkit Popup or Mopups. I had to adjust the animation of the Content
 *               part to to 10 so it just pops up in the center while the background slides down from the top. Still need to find
 *               a long explanation so I can make sure the size adjusts. mdail 3-7-24
 *  v 3.023.0233 The cards are working however the popup for the information shows up blank. Need to figure that
 *               out and fix it. Added Task.Run to many async task methods, got the de/select all buttons to work
 *               after moving all references to the collection view to the view and out of the model view  mdail 3-5-24
 *  v 3.023.0232 Needed to change release number to upload to the app store for beta test as it failed to upload
 *               the first time I tried, yet used to number mdail 2-29-24
 *  v 2.023.0231 NEED TO TEST ALL CASD FUNCTIONS MDAIL 2-23-24
 *  v 2.023.0230 Redo the code for the cards view (Flash, practice, quiz) to fix the issue with the app crashing when 
 *               going back and forth between the cards and the select screens no disposing of the CarouselView in 
 *               the cards view  properly with a System.ObjectDisposedException mdail 2-23-24
 *  v 2.022.0229 Added code to copy a new database if the version number not equal to the current version number. mdail 2-22-24
 *  v 2.022.0228 Fixed review after quiz not being able to tell correct, incorrect from skipped questions. by checking AnswerSelected
 *               first as it is 0 for skipped questions, it is also 0 for questions loaded from the review button on the main page until
 *               it is set by using the review value to finding where that answer is now. mdail 2-22-24
 *  v 2.022.0227 The new code was correctly saving the review number, however it wasn't selecting the correct index so
 *               I changed back to comparing the selected answer number to the shuffled answer numbers in if's to select the
 *               right incorrect answer mdail 2-22-24 
 *  v 2.022.0226 Had to redo the code to find and save the review field and to convert the review field to be the correct
 *               AnswerSelected to set the proper selections and colors, and instructions in the label to let the user
 *               study which answer they selected and which is the correct answer. NEED TO test it to make sure it 
 *               works correctly mdail 2-21-24
 *  v 2.022.0225 Set the name of the app to VA Accredited Exam Study Guide for all version of the app. mdail 2-21-24
 *  v 2.022.0224 The review is working from the popup, however when starting it from the main screen somehow Constants.REVIEW is
 *               getting set to false after I set it to true and the loading of the Quiz page. I need to find out why. mdail 2-14-24
 *  v 2.022.0223 Updated project to Net 8.0, and returned the review quiz page to a community popup. mdail 2-13-24
 *  v 2.022.0222 Trying to convert the Review Quiz Popup back to use the Mo-pups Popup page, however the instructions are not
 *               very clear. It takes using a ViewModel and I seen to be getting close, however right now I an having trouble
 *               figuring out how the close method needs to be called. Then I will need to figure out exactly how to get the
 *               responses from the buttons when they are clicked, which will need to call the close and act on which of the 
 *               buttons were clicked. mdail 11-16-23
 *  v 2.022.0221 Fix the background color of the practice and quiz in dark mode changing to a dark color in areas around the
 *               answer labels, also the background of the answer in the flashcard was not changing the the grey to insure users
 *               can easily tell the card flipped to the answer or is on the question. Mdail 11-14-23
 *  v 2.022.0220 Changed the Information Page popup to work better at resizing the popup, it's still a bit lager if the text
 *               is 7 lines or longer (it gets a little bit bigger with ever line added), however it at least seems to display
 *               all of the text now. Added a Util function FirstCharToUpper to make sure all string start with upper case
 *               letter in the Information page and for all questions and Answers. Also set the background color for the information
 *               page message label to yellow mdail 11-7-23
 *  v 2.022.0219 Changed the Review popup so it now display correctly, not how I originally wanted it however it works,
 *               Fixed the Navigations to navigate back to the how page when exit is clicked on the popup of the review.
 *               Need to try a grid layout for the information popup to see if I can get it to size correctly also. ALSO need 
 *               to figure out why the elapsed time isn't updating the label in the quiz screen now might need to move so of
 *               it to the model from the questions mdail 11-1-23
 *  v 2.022.0218 Changed the Review popup to use the community tool kit popup and get the true/false response mdail 10-31-23
 *  v 2.022.0217 Fixed the scoring so it scores correctly and add or subtracts from correct when appropriate mdail 10-31-23
 *  v 2.022.0216 Fixed the instruction label and the top select image button not displaying. Having INotifyPropertyChanged added 
 *               after the ContentPage at the top of the file as in QuizView : ContentPage, INotifyPropertyChanged caused it 
 *               to not work. ContentPage already implants INotifyPropertyChanged so adding it to the top caused it to no longer
 *               work for the properties that were assigned outside the CarouselView for some reason. It is totally stupid that this cost
 *               me almost a weeks work however it all seems to work now. The change to the way the button work also changes the way
 *               AnswerSelected is assigned which now means that the scoring was broken and I got a quiz score of -2. I fixed part of
 *               the scoring by adding LastSelectedAnswer and setting it at the top of AnswerPressed, CheckWasCorrect might be fixed
 *               however it needs to be tested and the whole scoring needs further testing. The ReviewQuizPopup doesn't wait for the answer
 *               so I need to do it with another kind of popup or do it differently for the review of the quiz mdail 10-26-23
 *  v 2.022.0215 Fix some of the color setting problems with practice and quiz, the Lock button in practice stays unset until
 *               the card is scrolled away and back again. I might have fixed it needs test. FIx the quiz to use the same code
 *               as the practice to set colors, added the instruction label text and text color an font size to the model mdail 10-20-23
 *  v 2.022.0214 Need to test color setting to see if it is doing more work than it needs to mdail 10-19-23
 *  v 2.022.0213 Fixed the C, D and lock buttons back color staying red when they should not have been being set, also fixed the
 *               same type of problem from happening in the quiz mdail 10-19-23
 *  v 2.022.0212 Added SetEnabled method in the view models to make sure that buttons were enabled or disabled as needed, the change
 *               to the way the colors are handle still needs more testing in both practice and quiz mode mdail 10-18-23
 *  v 2.022.0211 Changing the way the colors and images are changed and handled for all the buttons and text in the practice and
 *               quiz are handled by moving it all to the model of the question mdail 10-18-23
 *  v 2.022.0210 Fixed the button size and positions of the chevrons in all cards by adding a width request and changing the height
 *               to 40, the width is 120 so you no longer have to hit right where the chevron is mdail 10-17-23
 *  v 2.022.0209 Changed the prev & next button from image button to regular button with images on either the left or right side of
 *               transparent text sayings either prev or next so the buttons will take up more space mdail 10-17-23
 *  v 2.022.0208 Edited the way the answer is check in review mode to try to account for review when all questions are present after 
 *               quiz is take so correct answers show correct instead of skipped mdail 10-17-23
 *  v 2.022.0207 I don't know why it is close now, by rights it shouldn't be however it seems to be on android at least. I removed the 
 *               frame and changed the button to use a white background and navy text so it isn't obvious that sometimes it is too high.
 *               Note: the app wont start on windows it says something like the size is out of range when it tries to display the landscape
 *               version of the main page. I'll have to worry about that later mdail 10-16-23
 *  v 2.022.0206 After fixing the Interface so it runs, the numbers returned aren't correct. The width is way too same as is the height.
 *               Some of the problem might be the numbers don't match as in one might be px while the other is sp (for android). I also 
 *               don't thinks it is taking into account the fact that it will take more than one line for the text. mdail 10-13-23
 *  v 2.022.0205 Fixed the Explanation button disappearing once it was clicked, Still trying to fix the Explanation information popup not 
 *               properly sizing, it chops off the button if the message text is 3 lines or longer, even after I removed all horizontal and
 *               vertical alignment option except for horizontal text options. I added an if the text line was longer than  50 to set the height
 *               and width which will work if I can figure out what size to set it to as 300 by 150 was close still there was then empty space 
 *               below the exit button. It is really quite stupid that it doesn't size properly mdail 10-2-23
 *  v 2.022.0204 Changed to use the Community Toolkit popup, It came up to small once need to do full delete folder, restore dotnet and see
 *               if it works. I might have to so how figure out how to set the size before loading it depending on the text length for the
 *               Explanation popup mdail 10-10-23
 *  v 2.022.0203 I made the Mo-popups pages for the Citation and Explanation however they aren't Modal so they user can navigate and leave the
 *               popup on the display, also for some reason they are the width of the screen and don't seem to want to be smaller. mdail 10-6-23
  * v 2.022.0202 Got the icon placed fairly well in flashcard, quiz and practice. They appear and disappear when they should and the clicked
 *               event is working. The Alerts show with the correct data. I added a try catch around the code in a method in the quiz and practice
 *               for the reload current item due to an index out of range that appeared during debugging however it was after the page had exited
 *               so I don't think it's a real problem just slow execution mdail 10-6-23
 *  v 2.022.0201 Fix the Explanation & Citation buttons not hiding and showing the way they should have, fixed the field Explanation not 
 *               loading from database, moved the feedback button to the top of the flashcard, changed the button spacing on the flashcard,
 *               still need to fix the spacing for the quiz and practice icon at the top, Made sure the alert dialogs show when the button is 
 *               clicked. Need to make custom Alerts so I can control the colors, when run on my phone in dark mode the button on th alerts is
 *               hard to read, need to fix the spacing on the practice and quiz for the icons to match the flashcards mdail 10-5-23
 *  v 2.022.0200 The image for the buttons are displaying and I have them about where I want them, Then button click code works, however the 
 *               length to inVisible isn't working. WHen testing one had a citation and the alert showed however it didn't have an explanation
 *               and that button was still visible. I modified the code in the converters to make the value a var as string and return the 
 *               is not or empty or inverted and it;s not working. mdail 10-4-23
 *  v 2.022.0199 Added code for Citation and Explanation to either be a blank image or an image button to all of the cards, Fixed the review
 *               to set selected answer to the number of the current location of the answer that matches the one that was selected when the 
 *               quiz was originally taken mdail 10-4-23
 *  v 2.022.0198 Went back to using the quiz for the review, Fixed the default setting setting C to fail. Fixed clearing the collections
 *               when I needed them to not clear, working on testing the colors in review mode most seem to work, however I am having trouble
 *               with visual studio locking up in debug mode today. I need to figure out why some of the incorrect answers were saved as correct
 *               in the review field of the database, changed it so the not answered is a separate check from the answered and answer selected not
 *               being the correct answer to see if that fixes the issue. However I think the score is correct as I answered 9 wrong and skipped one
 *               still in review it has a one of them that says correct instead of skipped or incorrect I need to fix that I just think that is part of
 *               scoring now mdail 10-3-23
 *  v 2.021.0197 Removed the Clear for the Collection of Questions from the On Disappearing in all the pages as it is what was causing the 
 *               review to be blank I think, I think there was a slight delay in the actual clearing happening so it appeared as though there
 *               were still questions there until shortly after page loading started. Made a review quiz page for reviews mdail 10-3-23
 *  v 2.020.0196 Fixed the review quiz popup, Checked the scoring of the text and it seem to work, Change how the review - exit works
 *               from the popup to set the Constants.REVIEW and then closes, if not review then the quiz page should close after the navigation
 *               has removed everything down to the main page. The button weren't getting set correctly for the review though, I worked on fixing that.
 *               The Review from the main page still load a blank carousel view when the review questions is selected making harder to test the review
 *               button setting in the quiz page. Added clearing the review field of the questions before resetting the new one when scoring the quiz,
 *               the total elapsed time still didn't get set for the database and I may have fixed that mdail 9-29-23
 *  v 2.020.0195 The review quiz popup is for some reason putting the app into a break mode without any code running. I changed it
 *               so it is using the Constants Last Quiz as its data so it isn't using the overloaded class version of the load for the page,
 *               I need to test it to see if this fixes that problem. I don't really understand as it loaded before mdail 9-28-23
 *  v 2.020.0194 Had to get the AI to fix the Sort code as it would sort by answer number if it didn't sort any other way instead of 
 *               leaving it alone mdail 9-27-23
 *  v 2.020.0193 Changed the way that questions are sorted to use the sort function and made custom sorts for each, Added cod to add an * before the
 *               correct answer in debug mode mdail 9-27-23
 *  v 2.020.0192 Changed the QuizView to take a bool input, if formQuiz set ReviewQuiz to true and the Quiz view model set QuizQuestions to 
 *               use Constants.ReviewQuestions otherwise it uses Constants.Questions to try and fix the quiz view from displaying nothing mdail 9-26-23
 *  v 2.020.0191 Added code to ask if use really wants to exit the quiz without scoring it mdail 9-26-23
 *  v 2.020.0190 Added the color and image change code for the questions, it is actually in the question model and controls the quiz and
 *               practice views, the quiz view in review mode sets the lock when the card is scrolled into place mdail 9-26-23
 *  v 2.020.0189 Fix color from score not being applied to background of review popup mdail 9-25-23
 *  v 2.020.0188 Fix the review questions button being hidden when there were questions to review, I think I might have fixed 
 *               no data showing up in the quiz page when loaded from the review page mdail 9-22-23
 *  v 2.020.0187 Y/N/O was still not right so I went through it once again to see if I can get it to always con out in that order mdail 9-22-23
 *  v 2.020.0186 Finished doing the selected text/image areas for the practice and quiz so when the user selected the text of an answer it
 *               reverse the text and background colors and swaps the image, it also sets the button at the top. Likewise the Button also 
 *               sets the text area. I also converted the frame around the cards to a border so I could make it a little wider. I also
 *               converted the flashcard so it too uses the border instead of the frame mdail 8-22-23
 *  v 2.020.0185 Started setting up to be able to have the background color, image, and Text color change when items are selected by
 *               tapping in the text area in practice & quiz mode. As of now only the select/unselect happens, I can by setting the Locked
 *               = true on the record when scrolled into view use the Locked to be able to display the correct-incorrect colors also if I 
 *               decide I should do that, however I need to get it working first mdail 9-22-23
 *  v 2.020.0184 Fixed the Select quiz approximate question count to exclude definitions and dates, Fixed review view layout to display 
 *               properly, Fixed flyout menu top extending across the whole screen, Fixed the Review Popup not having binding, fix not passing
 *               the last review to the review popup, It's colors are still wrong and the bottom exit button is floating below the reset (maybe
 *               ok with that), Still need to figure out the popup or just use the view for the regular quiz review. Need to check out going in 
 *               and out of quiz several times then picking practice to see if that causes a lockup or whether my system just did that once mdail 9-21-23
 *  v 2.020.0183 Realizes that I hadn't finished the sort for 3 answer with true/false so I finished it mdail 9-21-23
 *  v 2.020.0182 Fixed/Tested hidden and flagged, fixed hidden so it reload the category counts after it hides a question and after clearing the
 *               hidden questions. Tested practice and quiz new layouts, and new touch recognizers to make sure they worked and selected the 
 *               correct letter at the top. Changed the review insert record to use the full sql command 
 *  v 2.020.0181 Changed the layout of the practice and quiz to use a stack layout around the label and image for the questions and answers
 *               and as tap gesture for the stack layouts instead of one for each of the labels and images mdail 9-21-23
 *  v 2.020.0180 Added sort answer for true/false in different case, added sort for tru/false/other as that case did not come back up
 *               I am not sure it worked as I planned. For some reason the insert isn't working, I need to write an insert for the review
 *               quiz. I might have fixed part of the review popup layout, the background color isn't being set properly to the percent 
 *               color in the popup it's navy for most of it and white fo the percent part. Added fix for the hidden removing categories and also
 *               made it reloaded the count after a question is hidden still need to test that and test to see if hide and flag are getting set 
 *               and check their sql to make sure they are returned. Also when the popup is closed the navigate back to root isn't working. mdail 9-20-23
 *  v 2.020.0179 Added code to fix the problem of crashing index-out-of-range when category added or removed for current list yet is
 *               still in the save cat list. Also added the code to reload the Categories & CategoriesFlash to update the counts
 *               when questions are hidden or unhidden mdail 9-19-23
 *  v 2.020.0178 Fixed the box not always going to the edge in the practice or quiz views, fixed quiz crashing on startup, fixed quiz
 *               returning definitions and dates, fixed the quiz instructions being the same as the practice including the lock button,
 *               fixed the score button not displaying the alert asking if you want to score the quiz and exit. The review popup doesn't
 *               have a full background and then disappeared and the loading popup can back, which I maybe I fixed, still need to be tested.
 *               Also fixed an alert that wouldn't have worked in the review screen. mdail 9-19-23
 *  v 2.020.0177 Fixed the Review, Flagged & Hidden field being named incorrectly in the sql statements the updated and selected them, fix
 *               hidden questions from crashing the select screen because it could mess up the count of the saved categories causing an index
 *               out of range error trying to reset the selected categories. I found it however because it might have been trying to set selected
 *               from before the change to remove the Dates & Definition Field from the practice list. I need to test more to see if 
 *               the crashing trying to load the select screen is really fixed mdail 9-14-23
 *  v 2.020.0176 Now in app it either runs _ = db.LoadCountDataAsync() then _ = db.LoadCountDataFlashOnlyAsync().ConfigureAwait(false) or
 *               dm.CopyDBIfNotExists() which after coping the db run the 2 methods above. Then in the SelectCatFlashView it assigns 
 *               sfvm.VM_Categories = new(Constants.CategoriesFlash) or sfvm.VM_Categories = new(Constants.Categories) depending 
 *               Flash which is true for flashcard false otherwise to make sure to include or exclude the dates and definitions 
 *               categories as they only are viable for flashcard as they only have one answer. It sets await to false on the second 
 *               of the data loads to speed up the loading of the app mdail 9-13-23
 *  v 2.020.0175 Removed Height & Width properties from the Questions model as I don't need them, Removed the Height, Width & Visibility
 *               Converters as I don't need them either. Added an observable property for Frame Padding to the Questions model as I might
 *               need it to set and reset the frame padding as I seems to change whether or not it works when set while it's running. I 
 *               had to update visual studio and reset the mac a couple of time to get the connection working mdail 9-13-23
 *  v 2.020.0174 For the most part I have the practice view & Quiz view working, The practice view loses center on rotate though,
 *               while the flash card, and quiz maintain it. Removing the Padding from the frame enabled the box to span the whole
 *               thing, until I stop and restart the app, then it falls short again. Sometime a lot sometimes its closer. I set the
 *               padding to 1 and it stays a little closer. The main thing that was causing problems with the text getting cut off
 *               was the scroll view around the grid. Moving it to be around the frame and there by the whole layout works better.
 *               Still trying to get the fine tuning done, however it's changing thing when I run it in debug mode or start the 
 *               app on the emulator makes things strange. Also the emulator is losing it's settings and the fact that the app 
 *               was previously installed between days for some reason. I need to fix that also. I wen back to using the XAML file 
 *               once I figured out the main problem. 9-12-23
 *  v 2.020.0173 Found that placing the scroll view around only the labels area cause it to act like a flex layout set for equal
 *               space around, tried moving the scroll view to around the whole frame and it seemed to fix the layout so it looked
 *               right, decided to try the same thing in xaml as I had the rest of the code for hiding & showing controls and
 *               setting colors etc. working for the XAML version and hadn't gotten that far in the code version yet mdail 9-12-23
 *  v 2.020.0172 Tried several variation, the scroll view only works if set to vertical FIllAndExpand, however that messes up all 
 *               the Heights of the Answers and Question. All other code attempts failed also. I am back where I started right 
 *               now except I changed the fire column to { Width = new GridLength(1, GridUnitType.Star) }, as all rows left 
 *               to default spaced column too far mdail 9-11-23
 *  v 2.020.0171 Got the grid to create and display, I'm still having trouble with the sizes of the labels and right now hiding
 *               the extra labels, images, boxes and button. I did get the top buttons to look as they should as well as the
 *               Q, feedback, hide and flag look as they should. For some reason, even though I am making sure that the Property
 *               Changed of the Model is firing the things are still not getting hidden. mdail 9-8-23
 *  v 2.020.0170 First attempt that displays, the next, prev and random buttons don't work that aren't code in yet, still out
 *               side of the carousel view. Right not the Q, feedback, hide and flag all on right, text doesn't grow for any fields
 *               however it all shows in correct order. Tried adding a grid to handle the top row and question text, so far it 
 *               failed, tried to measure the labels and the calc returned -1. mdail 9-7-23
 *  v 2.020.0169 Started to add the CarouselView as code behind instead of xaml so I can run the Height calculation on the label
 *               that is to go into the CarouselView instead of a new label not associated with the view, Also getting ChapGPT to
 *               help with the code to handle setting things to not show up text lengths are 0 and when the selections are already 
 *               done to reset the previous selections. 
 *  v 2.019.0168 Added the Utils to calculate the height and width of the labels for the practice card view and added it to the
 *               XAML. It should be ready to test mdail 9-5-23
 *  v 2.019.0167 Added Properties to the Questions model to hold Calculated label height and width to use to se the label height 
 *               for the Practice and Quiz view in xaml.
 *  v 2.018.0166 Still can't get the practice card (quiz either) to display all the card text data correctly all the time 
 *               I ask the AI for a layout and it gave me one I will try next, it is too simple I think though. mdail 9/1/23
 *  v 2.018.0165 Need to still figure out how to fix menu not scrolling in landscape mode, figure out how to disable touch when it
 *               is past the end of the menu. Fixed the version number being reset when the page rotated, ask text sizes to look 
 *               better with this apps title mdail 8-31-23
 *  v 2.018.0164 Fixed the main page in landscape so it is readable and usable, Fix the space for the ask row still being used after
 *               the ask view was closed mdail 8-31-23
 *  v 2.018.0163 Really fixed the ask having Smoking the Boards in the text fields, Fixed the layouts running off the screen after
 *               rotating to the screen for the second time. I have to recreate a new Portrait or Landscape pge on each rotation,
 *               however this caused a problem when trying to set the ask content part of the page so I had to check if the holders 
 *               content was not and if it is add it, however not add it if it wasn't null mdail 8-31-23
 *  v 2.018.0162 Update main page to use MainContentPortrait and MainContentLandscape to set for screen rotation however  it's
 *               not resizing correctly as of right now, Fixed the buttons on the select screen having black text. Verified that
 *               sometimes the select/unselect isn't working on the selectCatFlashView. Fixed the font icons not working, fixed 
 *               the screen crashing when not run from visual studio, Fixed the Ask screen saying Smoking the Boards mdail 8-30-23
 *  v 2.017.0161 Fixed the IAskView and got the ask to show up, found out that the app crashes when not in run from vs, and just
 *               from the icon mdail 8-29-23
 *  v 2.017.0160 Start reworking the Ask views as IAskView interface with shared commands mdail 8-28-23 
 *  v 2.016.0159 Added a row to the grids of the review page and popup for the Date:, Time: & Duration: titles for the labels, the
 *               Quiz Results spans 2 rows as does the correct of total questions line of the review also the review questions button. 
 *  v 2.016.0158 Added elapsed time to the database, review model, create review objects, saved review and display review popup
 *               and review page so it will be displayed mdail 8-24-23
 *  v 2.016.0157 Modified the elapsed time so it can be paused and resumed, when the user taps score quiz an alert is displayed to
 *               verify they really want to exit, if yes the timer is stopped and the quiz is scored, if no the timer is resumed and
 *               they are returned to the quiz mdail 8-24-23
 *  v 2.016.0156 Added the Ask portion of the project to ask the user for feedback or rating. also added a timer to the quiz page to
 *               track the elapsed time taken for the quiz. I need to update the database and the review screens to add the elapsed
 *               time reviews mdail 8-23-23
 *  v 2.015.0155 Fixed the menu screen so it only cover 2/3rds of the screen and is transparent around it, when a row is selected the 
 *               background changes grey for the visible section of the screen and stays transparent for the other part of the screen
 *               which means it actually changes to transparent and back to transparent again. Fix it so the Menu hamburger icon shows
 *               up on the left side of the screen and then moved it further to the left with a -20 left margin mdail 8-22-23
 *  v 2.015.0154 Changed the menu back to my original work around idea of a Mopups Popup page. ANd then had to send time fixing problem
 *               with loading and unloading it. Fixed the animation so it pops in from the left then back out the same way so it looks
 *               like a flyout menu. Fixed the selected color being orange and made it the background grey when the menu items were
 *               clicked. Added send feedback and rate app (NOTE: the rate app code still needs to be written). Fixed the bottom edge
 *               of the menu to make it have some transparency. mdail 8-15-23
 *  v 2.014.0153 Fixed Saved selected categories getting transferred from between practice and flashcards incorrectly if one was set 
 *               and the other wasn't when app first run. Added feature that when the label or the image of the answered are tapped 
 *               the corresponding letter image button is selected also. It still requires the lock pressed in practice mode to see 
 *               the correct answer. (NOTE: there are places in the answer area where the letter isn't selected I would like to figure 
 *               out how to fix that so the whole area in the answer reacts to touch) mdail 8-10-23
 *  v 2.013.0152 Added checks for 0 records for hidden and flagged flashcards only, fixed scoring falling because the } for the loop
 *               through the questions for setting then to needing to be reviewed was too far down somehow. fixed the icon with at least
 *               a temporary VDCA icon, fixed the splash screen with at least a temp screen, then fixed the splash screen 
 *               background color mdail 8-8-32 
 *  v 2.013.0151 Coded a work around for the hamburger icon not showing in the navigation bar by adding an icon to the right side of
 *               the screen an showing a page emulating a flyout page. Added the code to clear the review data, questions, flagged 
 *               and hidden. Added the code to show the quiz screen from the review quizzes screen to review the questions mdail 8-7-23
 *  v 2.012.0150 Remove the review quiz control as it didn't work, move the xaml to the review quiz view model and got it to work by
 *               assigning the last quizzes data in code as well as in the xaml. Looking at the code from smoking the boards it seems that
 *               the xaml bindings work in the collection view. I also assigned the lasts review in code in the review quiz popup that will
 *               display the score form the quiz I still need to test to see if the exit button works and the review button works, as well
 *               as testing the review quiz popup to see how the xaml work mdail 8-2-23
 *  v 2.011.0149 Add a Review Quiz Popup that will display the currently taken quiz after it has been scored and saved to the database, 
 *               there is then a button that sets the REVIEW constant to true and removes the popup so the user can review the answer to
 *               see the correct answers and which they got wrong. I still need to add an exit button that will close the popup and the 
 *               quiz page and maybe go all the way back to the main page or at least to the select quiz page mdail 8-1-23
 *  v 2.011.0148 Changed all times that the loading page is shown to call it from the loading page class in the utils folder. Moved the VDCA
 *               loading page to the Popups directory and assembly namespace. Moved unloading the loading page to in a finally of the catch
 *               in the select cat flash view model to make sure it runs even if there is an error mdail 8-1-23
 *  v 2.011.0147 Removed the bottom sheet as It cause problem with the choice letter buttons. Finished the review Quizzes page and setting the
 *               REVIEW constant to true so when going back to the quizzes or after loading the quizzes page the correct answer will show. I 
 *               still need to test it to make sure it works as well as really testing the scoring of the quiz. NOTE: I still need to 
 *               test the hidden and flagged feature and figure out how to show, and reset those cards too. mdail 7-27-23
 *  v 2.011.0146 Working on getting the Bottom Sheet to work, if I simply add a grid around the whole thing the the letter buttons
 *               don't appear and disappear as they should and aren't always centered. I'm going to have to see what happens if I
 *               replace the outer Stack layout with a grid to see if that allows it to work as I need it to. Other wise the review
 *               page should be done if it works as a control put into the outer page. I do have the command fixed I think. mdail 7-26-23
 *  v 2.011.0145 Finished up most of the work on the review page and it's content page, cleaned up a lot of code, fixed the Quiz View
 *               model not being in the view model name space. made a special class to load and unload the busy page, finished the 
 *               scoring part of the quiz and saving the review questions by setting the review int to the selected answer or 5 if
 *               the answer was skipped - added code to handle setting answer selected 5 to incorrect and noting that it 
 *               was skipped so the user know why it was wrong mdail 7-25-23
 *  v 2.011.0144 Added the review quiz table to the database and started working on the review quizzes view, put the main part of
 *               the view into a custom control so I can us it as a popup from the quiz view at the end of the quiz when the user
 *               scores the quiz mdail 7-21-23
 *  v 2.011.0143 Fixed performance issue by removing the stack view from around the Carousell view in all the cards mdail 7-19-23
 *  v 2.011.0142 Fixed ALL the buttons sometimes getting set the IsEnabled false when it needed to be true mdail 7-19-13
 *  v 2.011.0141 Fixed the buttons sometimes getting set the IsEnabled false when it needed to be true mdail 7-18-13
 *  v 2.011.0140 Fix the buttons not always holding there state when scrolling forward & back, made separate methods fo reach button
 *               Set & Reset and a SetSelectionButtonToDefault for all buttons. note that all button methods check the shuffled answer 
 *               lengths of answer 3 & 4 to make sure were not setting a property on a button that is invisible mdail 7-18-23
 *  v 2.010.0139 Fix the app crashing if not run in debug from vs, the flash card was due to a reference to a label type I had used
 *               in Smoking The Boards that I am not using here. The others were because for some reason it couldn't find the Width Convert
 *               until I created one in the Practice and Quiz page before the InitializeComponent command ran. mdail 7-18-23
 *  v 2.010.0138 Removed the code to hide/show the c &/or d buttons at page load as it is taken care of in set 0r reset button which
 *               is run when op position change fires. It seems to have fixed the crashing on quiz page loading, however at this
 *               point it requires more testing before I can be shore. Also the the letter pressed methods now call function the 
 *               check for correct, check to see if correct needs to be decremented as it was correct and is now incorrect and sets the 
 *               answered number. These need to be tested to see if they work as expected or still sometimes go off to never never land mdail 7-14-23
 *  v 2.010.0137 Correction, put the code to hide the answer selection button back in and remove the XAML to set them invisible if 
 *               labelC &/or labelD text length was 0 as it crash after I made the change below. mdail 7-14-23
 *  v 2.010.0136 Fixed intermittent crashing on load of practice or quiz if the first question to display had less than 4 questions,
 *               after I found out that if the first question had less the 4 then when a question with 4 showed up the buttons for would still
 *               be hidden, I added the code to hide or show then when changing questions. I also added the code to hide then at load which would
 *               cause the app to crash because the XMAL had already hidden them mdail 7-14-23
 *  v 2.010.0135 Did first try at scoring the quiz, the correct is added if correct choice was selected, answered is incremented if question
 *               correct is being answer for the first time and skipped is decremented. if the question isn't being answer for the first time
 *               answer correct is increments, will if it isn't the correct answer correct is decremented. Also made it so id review is false,
 *               if it is the select color is set if the question was answer for the selected answer button and only check for the 
 *               answer if it is in review mode mdail 7-11-23
 *  v 2.010.0134 Change the color of the progress loading page to a red circle mdail 7-11-13
 *  v 2.010.0133 Added the code to display the loading and unloading page will the questions data and category data is loading mdail 7-11-23
 *  v 2.010.0132 Added the Quiz view & view model empty shells to start working on writing the quiz code mdail 6-23-23
 *  v 2.009.0131 Changed the stack layout to a gird with one row and one column to get the label to stay centered when
 *               when the text is changed to correct or incorrect mdail 6-16-23
 *  v 2.009.0130 I finally got the practice view & view model to work like I want it to setting except I had to use the
 *               background color instead of the border color to express correct-incorrect. Got it to add & remove buttons 
 *               for 2 or 3 or 4 answer questions and save the responses for future scrolling. mdail 6-14-23
 *  v 2.009.0129 Had to change the use of border color to background color to indicate correct of not mdail 6-14-23
 *  v 2.008.0128 Added a border of width 2 and color navy to all the choice buttons so that the border will show when it is changed 
 *               to correct or incorrect after being answered, centered the text for the instructions so that when answered
 *               the correct or incorrect will be centered in the practice view 7 view model mdail 6-14-23 
 *  v 2.008.0127 Fixed 3 of 4 marking the answer correct when it was incorrect in the practice view model mdail 6-14-23
 *  v 2.008.0126 Added code to set and reset the buttons for correct=incorrect and unlocked, fixed correct & incorrect colors, fixed
 *               possible index out of range by adding extra check to see if the Answers array is long enough && 
 *               meets the other requirements  mdail 6-13-23
 *  v 2.008.0125 Added Code to show correct-incorrect selections in the practice view model and remember them after scrolled away mdail 6-13-23
 *  v 2.008.0124 Had to add getters for individual Shuffled answers to return the answers from the answers list so the the XAML would
 *               not fail with index out of range and display all 4 fields when there were less answers mdail 6-9-23
 *  v 2.008.0123 Changed the Value Tuple array to a list of ValueTuples and rewrote the code to shuffle and sort the tupple list, also
 *               update the practice view to display the Answers (ValueTuple list) as the answers on the screen mdail 6-9-23
 *  v 2.007.0122 Added the code to shuffle the Answer ValueType from the questions in the Questions database class and the to arrange the
 *               Answer if All of these-Non of these or True-false mdail 6-8-23
 *  v 2.007.0121 Removed the flex layout from the practice card to get the grid to give each row the height it needs, set the selection page
 *               to scroll to the last selected item in the saved list on load, fixed the selection page sometimes not properly handling
 *               the first selected item (I had forgot to reset the selected item property of the collection view to null mdail 6-7-23
 *  v 2.006.0120 Right now I'm getting inconsistent results from the changing selection after some have been preset, sometime it will ignore the
 *               first item that is deselected, then it can't be selected or deselected after that. I am getting confusing results from the layout
 *               also. Sometimes the text wraps fine, other time it wrap ok in some cells and not others (it runs off the screen before wrapping the
 *               text). It seem that the cell heights are set the same for all 5 groups of two rows and that seems to be causing the problems. I was
 *               going to try removing the flex view to see if that changes things. mdail 6-6-23
 *  v 2.006.0119 Fixed the cards saving the cards from the last selection and add the new cards to the list if the selection page is returned to 
 *               after viewing flash or practice cards and making new selection and going back into the cards. mdail 6-6-23
 *  v 2.006.0118 Remove unnecessary using statements, and fix some missing code from the practice view needed to compile, simplified the send
 *               messages to one call from the view removing the call to the view models mdail 6-5-23
 *  v 2.006.0117 Finished the custom selectable image button (Maybe) and started getting the practice view ready to start testing. I did the layout
 *               for the practice view with a row a selectable image buttons for  A, B, C, D and Lock which will be use to answer the questions, then 
 *               clicking the lock locks the answer in and displays whether or not the answer was correct (still need the code to make that work). 
 *               I need to test the layout to see if it will work especially with the long question and answers. Also add a few classes to the Utils 
 *               group to be able to reuse more of the code from one set of view/view models to the other by running the code in the Utils class. mdail 6-2-23
 *  v 2.006.0116 Changed the way I was trying to build a custom control to the Selectable Image class which is a content view with an image button
 *               that has bindable properties for switching images, background and border colors when selected mdail 6-1-23
 *  v 2.005.0114 Change the random animation so the card acts more like a regular one card swipe, Add a Shuffle extension to List, this allows me
 *               to shuffle the questions for the flashcards, practice and quizzes, Added the Practice cards view & view model and started them mdail 5-26-23
 *  v 2.005.0113 The Build and version number return backwards, fixed the font being too large for the build number to show properly on the main page mdail 5-23-23
 *  v 2.005.0112 Fixed the progress bar not showing the progress mdail 5-23-23
 *  v 2.005.0111 Fixed previous, next and random not working from the buttons the first time, fixed next going too far and giving index out of range,
 *               fixed SelectedCategories not tracking removed items properly, fix flip not reset when scrolled way from the card mdail 5-23-23
 *  v 2.005.0110 Fix the display so the cards show up the correct size with out have a size set, fixed the flip so it show the correct size,
 *               fix the flp losing the letter A image and shifting the flip image to the left. The scroll works as do the previous, random and next
 *               button, however for some reason the next button doesn't work on the first try (I did fix the next button causing the app the 
 *               go off into limbo by removing the CurrentIndex property from the ViewModel. I need to figure out why the scroll doesn't work the
 *               first time the button is pressed and only after the screen is scrolled. Its might be that there is a scroll view in the as well as the
 *               CarouselView, I need to test that idea out mdail 5-19-23
 *  v 2.004.0109 For some reason the carousel view is not showing up at all I need to fix that add figure out what is wrong mdail 5-16-23
 *  v 2.004.0108 Finish the UI for the flashcard, however I lost the ability to change the Icon colors (Note: it wouldn't have worked in windows anyway.
 *               Also did all or most of the code in the view and view model for the flashcard mdail 5-16-23
 *  v 2.004.0107 Built most of the first attempt at the UI for the flashcard, still need to add the right and left arrows at the vary bottom mdail 5-15-23
 *  v 2.004.0106 Deleted the view cell attempted view and the subfolder that went with it. Fixed the sql statement to get the list of 
 *               selected questions for the selected categories, fix the call to change pages, need to fix the xaml for the flashcard view 
 *               as right now flashcard view doesn't open, need to add a VDCA loading view on the main page prior to opening the select category page.
 *               Need to design the layout for the Flash card view xaml, added the svg image file for the chevron icons right and left. for the  mdail 5-10-23
 *  v 2.004.0105 Clean up old remark code, moved code to the view model of the select cat flash view to make the code easier to read 
 *               and more MVVM. Also fix misspelled categories in the database so they display correctly mdail 5-4-23
 *  v 2.004.0104 When loading the saved items to the collection view we have to set the category item's selected (ItIsChecked) to false
 *               because once we set the collection view selected item to that category item the CollectionView_SelectionChanged event fires
 *               and as it handles flipping the ItIsChecked, ItIsChecked needs to be false when sent because then it gets set to true mdail 5-4-23
 *  v 2.004.0103 Fixed the code to add or remove the selected category from the list as I had the check to see if the count was 
 *               greater than 0 in the wrong place mdail 5-4-23
 *  v 2.004.0102 Went back to CollectionView, changed the labels in the collection view to be in a grid instead of 2 stack layouts,
 *               started code to track the selected items in the collection view myself using single selection and then setting the 
 *               selected item to null, the color for the background is handle by binding the background and checkbox colors to properties
 *               in the model for the categories. Fix the xaml of saying the a categories observable collection failed to bind by renaming the 
 *               collection in the view model mdail 5-3-23
 *  v 2.003.0101 List view was worse, I think I am going to have to set the collection view to single selection and handle the
 *               selected items and setting the colors of the items for selected or not myself mdail 5-2-23
 *  v 2.003.0093 Try using a list view instead of a collection view which isn't tracking selected items correctly mdail 4-25-23 
 *  v 2.002.0092 Moved to Mentor bitbucket because I can't get to it from mine on my Mac. mdail 4-18-23
 *  v 2.001.0091 start to set it up so that the practice and flash card use the same view to select the VM_Categories. Then they will
 *               send the app to a different page depending on flash being true for flash card or false for practice mdail 4-6-23
 *  v 2.001.0090 set up the save abd loading of the previous selected item for the select cat
 *  v 2.001.0089 Got the list for selecting VM_Categories and the selected check mark and background working mdail 4-5-23
 *  v 2.001.0088 Added the Select Category for Flash  view and view model and start designing the page layout and adding methods to make 
 *               it all work mdail 4-4-34
 *  v 2.001.0087 move the database to the raw subdirectory of the resources directory so it copies into the app bundle properly mdail 4-1-23
 *  v 2.001.0086 fix the sql so it works on all platforms by installing the SQLitePCLRaw.bundle_green nugget as it seems that sqlite-net-pcl  
 *               nugget is using an older version mdail 4-1-23
 *  v 2.001.0085 fix the file copy so it works properly to copy the database to the local directory on the appropriate systems mdail 4-1-23
 *  v 2.000.0000 - Start VDCA app 3/1/23 mdail
 * 
 */