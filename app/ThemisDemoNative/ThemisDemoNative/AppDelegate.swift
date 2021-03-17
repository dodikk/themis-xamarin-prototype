//
//  AppDelegate.swift
//  ThemisDemoNative
//
//  Created by dodikk on 17.03.2021.
//

import UIKit
import themis


@main
class AppDelegate: UIResponder, UIApplicationDelegate
{
    func application(         _ application: UIApplication,
didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool
    {
        // Override point for customization after application launch.
        
        executeThemisNativeSample()
        
        return true
    }

    func executeThemisNativeSample()
    {
        let masterKey = "UkVDMgAAAC13PCVZAKOczZXUpvkhsC+xvwWnv3CLmlG0Wzy8ZBMnT+2yx/dg";
        let masterKeyData = Data(
            base64Encoded:masterKey,
            options: .ignoreUnknownCharacters)!;
        
        let cellSeal = TSCellSeal(key: masterKeyData);
        let initialMessagePlainText = "iOS native project plain text message";
        let initialPlainTextData = initialMessagePlainText.data(using: .utf8)!;
        print("initial text: \(initialMessagePlainText)");
        
        let cipherText = try! cellSeal!.encrypt(initialPlainTextData);
        print("encrypted text: \(cipherText)");
        
        let resultPlainTextData = try! cellSeal!.decrypt(cipherText);
        
        let resultPlainText = String(
            data: resultPlainTextData,
            encoding: .utf8)!
        print("decryptedText : \(resultPlainText)")
        
    }
    
    // MARK: UISceneSession Lifecycle

    func application(_ application: UIApplication, configurationForConnecting connectingSceneSession: UISceneSession, options: UIScene.ConnectionOptions) -> UISceneConfiguration {
        // Called when a new scene session is being created.
        // Use this method to select a configuration to create the new scene with.
        return UISceneConfiguration(name: "Default Configuration", sessionRole: connectingSceneSession.role)
    }

    func application(_ application: UIApplication, didDiscardSceneSessions sceneSessions: Set<UISceneSession>) {
        // Called when the user discards a scene session.
        // If any sessions were discarded while the application was not running, this will be called shortly after application:didFinishLaunchingWithOptions.
        // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
    }


}

