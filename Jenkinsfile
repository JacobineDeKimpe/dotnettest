pipeline{
agent any
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
                
                    build 'dotnetapp'
                    
         
                }
            }
        }
stage("publish") {
            
            steps {
                echo "publish the app"
                script {
                    
                }
            }
        }        
   }      
} 
