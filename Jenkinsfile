pipeline{
agent { label 'webserver' }
stages {
        stage('Checkout') {
            steps {
               git branch: 'main', credentialsId: 'RISE', url: 'git@github.com:JacobineDeKimpe/dotnettest.git'
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
stage("publish") {
            
            steps {
                echo "publish the app"
                script {
                    sh 'dotnet publish -c Release -o ./publish '
                }
            }
        }        
   }      
} 
