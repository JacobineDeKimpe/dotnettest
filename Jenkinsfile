pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', credentialsId: 'RISE', url: 'git@github.com:JacobineDeKimpe/dotnettest.git'
            }
        }

        stage('Build') {
            steps {
                echo "Building the application"
                script {
                    sh 'chmod +x ./build.sh'
                    sh './build.sh'
                }
            }
        }

        stage('Publish') {
            steps {
                echo "Publishing the application"
                script {
                    // Add publishing steps here
                    sh './publish.sh'
                }
            }
        }
    }
}
