#!/usr/bin/env groovy

pipeline{
agent { label 'built-in' }
stages {
stage('Checkout') {
    steps {
        git branch: 'main', credentialsId: 'Jenkins2', url: 'git@github.com:JacobineDeKimpe/dotnettest.git'
           }
        }    
stage('Build') {
    steps {
        script {
        sh 'ip a'
        sh 'apt-get update '
        sh 'apt-get update '
         sh 'apt-get install -y dotnet-sdk-8.0'
         echo "restore"
        sh 'dotnet restore'
         echo "build"
        sh 'dotnet build'
        
            }
    }
    }
}
}
