# TicTacToe Workshops

|   App   |             Status             |
| :------ | :----------------------------- |
| Android | ![Build status][badge-android] |
| iOS     | ![Build status][badge-ios]     |
| Windows | ![Build status][badge-windows] |

## Preparing

We have a quick look at creating a mobile app and hooking it up to CI/CD in
just a few seconds. This is not particularly Xamarin-specific, as it works for
all app types, but looks really cool because CI is producing a green tick
after no work at all!

### Preparing the repository

This section shows you how to create a new repository in the cloud so that CI
can do its job, for source history, and, for backups.

 1. Make sure you have an account on https://github.com/
 1. Create a new GitHub repository
 1. Clone the empty repository to disk
 1. Create a new **Mobile App (Xamarin.Forms)** solution
 1. Commit the solution to the repository and push

### Preparing App Center

This section shows you how to set up the simplest of CI that will build the
app every time you commit code to the repository.

 1. Make sure you have an account on https://appcenter.ms/
 1. Create a new organization, or use the account organization _(optional)_
 1. Create 3 new apps, for Android (Xamarin), iOS (Xamarin) and Windows (UWP)
 1. For each app, connect to the GitHub repository so it starts to build

## Creating a basic UI

This next section is getting to know some of the basics of Xamarin.Forms and
creating an actual app that will run on a device that does something other
than just sit there. It shows how to add controls, hook up events and do basic
navigation.

### Creating the first page

For the very first page, we don't actually need to create it as there is
already the default template page. But, we still need to customize it for the
launch screen.

> TODO: steps to create the basic launch screen

[badge-android]: https://build.appcenter.ms/v0.1/apps/61775db2-58b0-403b-acba-a4f9bc5f3020/branches/master/badge
[badge-ios]: https://build.appcenter.ms/v0.1/apps/e545948f-9929-4f7c-a084-8a09393d34e7/branches/master/badge
[badge-windows]: https://build.appcenter.ms/v0.1/apps/01712b65-92b8-47a5-9fe8-078ac0ccfa55/branches/master/badge
