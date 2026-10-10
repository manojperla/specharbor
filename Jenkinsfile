pipeline {
  agent { label 'linux-docker-dotnet' }
  options { timestamps(); timeout(time: 20, unit: 'MINUTES'); disableConcurrentBuilds() }
  stages {
    stage('Checkout') { steps { checkout scm } }
    stage('Acceptance specifications') { steps { sh 'bash ci/jenkins.sh' } }
  }
  post {
    always {
      junit testResults: 'TestResults/acceptance.xml', allowEmptyResults: false
      archiveArtifacts artifacts: 'TestResults/**/*,tests/**/artifacts/**/*', allowEmptyArchive: true
    }
  }
}
