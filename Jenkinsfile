pipeline{
agent any
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
   }      
} 
