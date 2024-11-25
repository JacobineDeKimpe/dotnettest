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
                    // Replace this with the actual build command for your project
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
