pipeline{
agent { label 'webserver' }
stages {
        stage('Checkout') {
            steps {
               git branch: 'main', credentialsId: 'jenkins', url: 'git@github.com:JacobineDeKimpe/dotnettest.git'
            }
        }

stage("build") {
            
            steps {
                echo "build the app"
                script {
                
                    sh 'dotnet restore'
                    sh 'dotnet build'
         
                }
            }
        }
stage("test") {
            
            steps {
                echo "test the app"
                script {
                    sh 'dotnet add package Microsoft.Playwright.NUnit' 
                    powershell 'pwsh bin/Debug/net8.0/playwright.ps1 install'    
                    sh 'dotnet test'
                }
            }
        }   
stage("publish") {
            
            steps {
                echo "test the app"
                script {
                    sh 'dotnet publish -c Release -o ./publish '
                }
            }
        }        
   }      
} 
