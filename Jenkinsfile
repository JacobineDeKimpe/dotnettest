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
         echo "restore"
        sh 'dotnet restore'
         echo "build"
        sh 'dotnet build'
        
        }

    }
}
}
