pipeline{
    agent any
stages {
stage('Checkout') {
    steps {
        git branch: 'main', credentialsId: 'Jenkins2', url: 'git@github.com:JacobineDeKimpe/dotnettest.git'
           }
        }    
stage('Build') {
    steps {
        echo "install dotnet"
        sh 'sudo dnf install dotnet-sdk-8.0 -y'
         echo "restore"
        sh 'dotnet restore'
         echo "build"
        sh 'dotnet build'
        
        }

    }
}
}
