pipeline{
stages {
        stage('Checkout') {
            steps {
               git branch: 'main', credentialsId: 'jenkins', url: 'git@github.com:JacobineDeKimpe/dotnettest.git'
            }
        }
} 
