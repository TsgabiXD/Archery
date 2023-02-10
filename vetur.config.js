// Info https://vuejs.github.io/vetur/reference/#detail
// https://github.com/vuejs/vetur/blob/master/rfcs/001-vetur-config-file.md
/** @type {import('vls').VeturConfig} */
/* eslint-disable */
module.exports = {
    settings: {
      "vetur.format.defaultFormatterOptions": {
        prettier: {
          singleQuote: true,
          trailingComma: "all",
        },
      },
    },
    projects: [
      {
        root: "./Archery.Web",
        // It is relative to root property.
        package: "./package.json",
      },
    ],
  };
  