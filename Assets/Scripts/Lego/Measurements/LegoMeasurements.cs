public static class LegoMeasurements
{

    // This class contains conversion ratios to convert between the different units. 
    //Simply multiply a measurement of the left unit to obtain a measurement of the right unit.
    public static class CONVERSIONS
    {
        public const double METER_TO_MILLIMETER = 0.001;
        public const double METER_TO_LDU = 0.0004;
        public const double MILLIMETER_TO_METER = 1000;
        public const double MILLIMETER_TO_LDU = 0.4;
        public const double LDU_TO_MILLIMETER = 2.5;
        public const double LDU_TO_METER = 2500;
    }

    public static class RATIO
    {
        public const double STUD_HEIGHT_TO_TOTAL_HEIGHT = LDU.STUD_HEIGHT / LDU.BRICK_HEIGHT_TOTAL;
        public const double BRICK_HEIGHT_NO_STUD_TO_TOTAL_HEIGHT = LDU.BRICK_HEIGHT_NO_STUD / LDU.BRICK_HEIGHT_TOTAL;
    }

    // Measurements
    public static class METER
    {
        /**************************/
        /* 1x1 Lego Measurements */
        /**************************/
        // Standard Lego Measurements
        public const double LEGO_WIDTH = 0.008; // X or Z Size
        public const double LEGO_DEPTH = 0.008; // X or Z Size

        // Stud Measurements
        public const double STUD_HEIGHT = 0.0016;
        public const double STUD_RADIUS = 0.0024;
        public const double STUD_DIAMETER = 0.0048;

        // distances between two adjacent studs
        public const double DISTANCE_BETWEEN_STUD_CENTERS = 0.008;
        public const double DISTANCE_BETWEEN_STUD_EDGES = 0.0032;

        /**************************/
        /* 1x1 Brick Measurements */
        /**************************/
        public const double BRICK_HEIGHT_NO_STUD = 0.0096; // Y Size
        public const double BRICK_HEIGHT_TOTAL = BRICK_HEIGHT_NO_STUD + STUD_HEIGHT;

        /**************************/
        /* 1x1 Plate Measurements */
        /**************************/
        public const double PLATE_HEIGHT = 0.0032; // Y Size
    }
    public class MILLIMETER
    {
        /**************************/
        /* 1x1 Lego Measurements */
        /**************************/
        // Standard Lego Measurements
        public const double LEGO_WIDTH = 8; // X or Z Size
        public const double LEGO_DEPTH = 8; // X or Z Size

        // Stud Measurements
        public const double STUD_HEIGHT = 1.6;
        public const double STUD_RADIUS = 2.4;
        public const double STUD_DIAMETER = 4.8;

        // distances between two adjacent studs
        public const double DISTANCE_BETWEEN_STUD_CENTERS = 8;
        public const double DISTANCE_BETWEEN_STUD_EDGES = 3.2;

        /**************************/
        /* 1x1 Brick Measurements */
        /**************************/
        public const double BRICK_HEIGHT_NO_STUD = 9.6; // Y Size
        public const double BRICK_HEIGHT_TOTAL = BRICK_HEIGHT_NO_STUD + STUD_HEIGHT;


        /**************************/
        /* 1x1 Plate Measurements */
        /**************************/
        public const double PLATE_HEIGHT = 3.2; // Y Size
    }
    public class LDU
    {
        /* 
          LDU is a simple unit that can simplify the measurements of various lego measurements.
          1 LDU = 0.4 mm;   
        */

        /**************************/
        /* 1x1 Lego Measurements  */
        /**************************/
        // Standard Lego Measurements
        public const double LEGO_WIDTH = 20; // X or Z Size
        public const double LEGO_DEPTH = 20; // X or Z Size

        // Stud Measurements
        public const double STUD_HEIGHT = 4;
        public const double STUD_RADIUS = 6;
        public const double STUD_DIAMETER = 12;

        // distances between two adjacent studs
        public const double DISTANCE_BETWEEN_STUD_CENTERS = 40;
        public const double DISTANCE_BETWEEN_STUD_EDGES = 8;

        /**************************/
        /* 1x1 Brick Measurements */
        /**************************/
        public const double BRICK_HEIGHT_NO_STUD = 24; // Y Size
        public const double BRICK_HEIGHT_TOTAL = BRICK_HEIGHT_NO_STUD + STUD_HEIGHT;


        /**************************/
        /* 1x1 Plate Measurements */
        /**************************/
        public const double PLATE_HEIGHT = 8; // Y Size
    }
}