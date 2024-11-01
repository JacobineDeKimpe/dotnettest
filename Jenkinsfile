pipeline{
stages {
stage('Checkout') {
    steps {
        git branch: 'main', credentialsId: 'Jenkins2', url: 'git@github.com:JacobineDeKimpe/dotnettest.git'
           }
        }    
stage('Build') {
script {
sh echo "restore"
dotnet restore
sh echo "build"
dotnet build
    
}

}
}
}