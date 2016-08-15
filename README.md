# Xamarin Forms Starter

A reasonably lightweight Xamarin Forms starter project.

This should be a decent place to start when writing Xamarin Forms apps using MVVM.

It's slightly opinionated, but these are the tools I tend to use in many projects - feel free to swap in your own.

There's a PS script included that'll find/replace/rename everything to your chosen solution name.

# What's Included

1. Dependency Injection (Autofac)
2. MVVM Framework
3. Secrets
4. Navigation Service
5. HockeyApp Integration.

# Platform Support

- iOS
- Android
- UWP
- Windows Phone 8.1
- Windows 8.1

# Dependencies

- Acr.UserDialogs
- PropertyChanged.Fody
- RestSharp Portable
- HockeyApp SDKs
- Splat
- Newtonsoft.Json
- Xamarin Forms
- Microsoft HTTP Client Libraries

# Usage

1. Run the setup script.
2. Copy config.secrets.example to config.secrets (ensure it's an Embedded Resource).
3. Optionally populate the HockeyAppId entry in config.secrets.
4. Add items to the Views and ViewModels directories and register them in Bootstrap.cs.
5. F5

# Screenshots

![screenshot](http://i.imgur.com/b4CiNN1.png)
