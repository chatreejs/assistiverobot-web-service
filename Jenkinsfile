pipeline {
      agent any
      stages {
          stage('Build') {
              steps {
                  bat 'dotnet build AssistiveRobot.Web.Service.sln'
              }
          }
          stage('Deploy for production') {
              when {
                  branch 'master'
              }
              steps {
                  bat 'appcmd stop apppool /apppool.name:\"Toktak Web Service\"'
                  bat label: '', script: '''cd ./AssistiveRobot.Web.Service
                                         dotnet restore
                                         dotnet publish --configuration Release --output D:\\Toktak-Web-Service\\production'''
                  bat 'appcmd start apppool /apppool.name:\"Toktak Web Service\"'
              }
          }
          stage('Deploy for development') {
              when {
                  branch 'dev'
              }
              steps {
                  bat 'appcmd stop apppool /apppool.name:\"Toktak Web Service Dev\"'
                  bat label: '', script: '''cd ./AssistiveRobot.Web.Service
                                         dotnet restore
                                         dotnet publish --configuration Release --output D:\\Toktak-Web-Service\\develop'''
                  bat 'appcmd start apppool /apppool.name:\"Toktak Web Service Dev\"'
              }
          }
      }
}