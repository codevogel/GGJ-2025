#!/bin/zsh
# Set this script as the external editor in Preferences > External Tools
# then add ` +$(Line) $(File) $(Column) ` to the arguments
wezterm start --cwd ~/GGJ-2025 -- zsh -c "source ~/.zshrc; nvim $1 $2 $3; zsh"

