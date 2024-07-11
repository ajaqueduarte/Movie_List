/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./Pages/**/*.cshtml'],
    daisyui: {
        themes: ['light', 'dark', 'cupcake'],
    },
  theme: {
    extend: {},
  },
    plugins: [
        require('daisyui'),
    ],
}

