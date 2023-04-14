# Justfile is a command runner inspired by Makefiles.
# 
# It can be installed on windows using choco or scoop
#   Ex: choco install just
#   Ex: scoop install just
# 
# On linux, it's available for the majority of package managers
#   Ex: sudo apt install just
#
# Running 'just --list' will give a list of commands
# that can be executed.


list-commands:
  @just --list

run-dev:
  docker-compose up

[unix]
trust-cert:
  ./scripts/trust-cert.sh -s

[windows]
trust-cert:
  dotnet dev-certs https -ep ${USERPROFILE}\.aspnet\https\aspnetapp.pfx
  dotnet dev-certs https --trust